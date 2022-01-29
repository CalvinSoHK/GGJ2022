using ChoiceManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utility.MessageQueue;

namespace CountManagement
{
    public class CountWindowController : MonoBehaviour
    {
        [SerializeField]
        private GameObject countWindow;

        public static string SHOW_COUNT = "ShowCount";

        public static string HIDE_COUNT = "HideCount";

        public static string COUNT_COMPLETE = "CountComplete";

        [Tooltip("Prefab image of a mark for character 1")]
        [SerializeField]
        private GameObject countPrefab1;

        [Tooltip("Prefab image of a mark for character 2")]
        [SerializeField]
        private GameObject countPrefab2;

        [Tooltip("Where the markers will spawn")]
        [SerializeField]
        private Transform targetTransform;

        /// <summary>
        /// Which prefab we are going to use
        /// </summary>
        private GameObject usedPrefab;

        /// <summary>
        /// Original count to show when window shows
        /// </summary>
        private int originalCount = -1;

        /// <summary>
        /// Target count to show when animation is done
        /// </summary>
        private int targetCount = -1;

        /// <summary>
        /// Breaks coroutines when set to true
        /// </summary>
        private bool breakCoroutine = false;

        /// <summary>
        /// Target positions for all markers
        /// </summary>
        private List<MarkerInfo> markerList = new List<MarkerInfo>();

        [Range(0, 1)]
        [Tooltip("Smooth time for animation")]
        [SerializeField]
        private float smoothTime;

        [Tooltip("Wait time for when it shows the value before reversing")]
        [SerializeField]
        private int waitTimeBeforeReversing = 1000;

        [Tooltip("Wait time for when it shows the value before the next part")]
        [SerializeField]
        private int waitBetweenOriginalAndTargetInSeconds = 1000;

        private void OnEnable()
        {
            MessageQueuesManager.MessagePopEvent += HandleMessage;
        }

        private void OnDisable()
        {
            MessageQueuesManager.MessagePopEvent -= HandleMessage;
        }

        private void HandleMessage(string id, string message)
        {
            if (id.Equals(MessageQueueID.UI))
            {
                if (message.Equals(SHOW_COUNT))
                {
                    countWindow.SetActive(true);
                }
                else if (message.Equals(HIDE_COUNT))
                {
                    countWindow.SetActive(false);
                }
            }
            else if (id.Equals(MessageQueueID.SELECTION))
            {
                ChoiceMessageObject obj = JsonUtility.FromJson<ChoiceMessageObject>(message);
                originalCount = obj.OriginalCount;
                targetCount = obj.TargetCount;
                switch (obj.ChosenCharacter)
                {
                    case CharacterEnum.Character1:
                        usedPrefab = countPrefab1;
                        break;
                    case CharacterEnum.Character2:
                        usedPrefab = countPrefab2;
                        break;
                    default:
                        throw new System.Exception("CountWindowController Error: Somehow selected None.");
                }
                AnimateCount();
            }
        }

        /// <summary>
        /// Animates markers with code
        /// </summary>
        private async void AnimateCount()
        {
            //Ensure we are the only coroutine
            breakCoroutine = true;
            await Task.Delay((int)(Time.deltaTime * 1000));
            breakCoroutine = false;

            //Animate marker count to original count
            await AnimateMarkerCount(originalCount, true);

            await Task.Delay(waitBetweenOriginalAndTargetInSeconds);

            //Animate marker count to target count
            await AnimateMarkerCount(targetCount, false);

            //Push out message that we finished animating
            Singleton.Instance.GetComponent<MessageQueuesManager>().TryQueueMessage(
                MessageQueueID.GAMESTATE,
                COUNT_COMPLETE
                );
        }

        /// <summary>
        /// Animates markers to target count.
        /// Reverse to origin if true
        /// </summary>
        /// <param name="_targetCount"></param>
        /// <param name="reverseAfter"></param>
        /// <returns></returns>
        private async Task AnimateMarkerCount(int _targetCount, bool reverseAfter)
        {
            //Clear out list
            foreach(MarkerInfo info in markerList)
            {
                Destroy(info.marker);
            }
            markerList.Clear();

            //Pivot index
            int pivot = _targetCount / 2;

            //Populate with the required number
            for (int i = 0; i < _targetCount; i++)
            {
                Vector3 targetPos;
                //If odd
                if (_targetCount % 2 != 0)
                {
                    targetPos = new Vector3((i - pivot) * 100, 0, 0);
                }
                else
                {
                    targetPos = new Vector3((i - pivot + 0.5f) * 100, 0, 0);
                }

                //Spawn marker on transform and set local pos to zero
                GameObject marker = GameObject.Instantiate(usedPrefab, targetTransform);
                marker.transform.localPosition = Vector3.zero;

                //Add to list
                markerList.Add(new MarkerInfo(
                    marker,
                    targetPos
                    ));
            }

            //Enable the window
            countWindow.SetActive(true);

            await AnimateMarkers();

            await Task.Delay(waitTimeBeforeReversing);

            if (reverseAfter)
            {
                //Send back to zero
                foreach(MarkerInfo info in markerList)
                {
                    info.targetPos = Vector3.zero;
                }

                await AnimateMarkers();
            }
        }

        private async Task AnimateMarkers()
        {
            while (!breakCoroutine)
            {
                //Distance from being done
                float distance = 0f;

                //Smooth damp all markers
                foreach (MarkerInfo info in markerList)
                {
                    info.marker.transform.localPosition = Vector3.SmoothDamp(
                        info.marker.transform.localPosition,
                        info.targetPos,
                        ref info.refVelocity,
                        smoothTime
                        );
                    distance += Vector3.Distance(info.marker.transform.localPosition, info.targetPos);
                }

                //Divide distance by number of markers
                distance /= markerList.Count;

                //If we are within 0.01 distance we can assume we are done
                if (distance <= 0.01f || markerList.Count <= 0)
                {
                    break;
                }

                //Otherwise yield and continue loop
                await Task.Delay((int)(Time.deltaTime * 1000));
            }
        }

        /// <summary>
        /// Class for storing prefabs and their target positions
        /// </summary>
        private class MarkerInfo
        {
            public GameObject marker;
            public Vector3 targetPos;
            public Vector3 refVelocity = Vector3.zero;

            public MarkerInfo(GameObject _marker, Vector3 _targetPos)
            {
                marker = _marker;
                targetPos = _targetPos;
            }
        }
    }
}
