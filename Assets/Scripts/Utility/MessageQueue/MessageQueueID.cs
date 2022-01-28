using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility.MessageQueue
{
    public class MessageQueueID
    {
        //Below are IDs to be used in the message queue manager

        public static string GAMESTATE = "GameState";

        public static string DIALOGUE = "Dialogue";

        public static string SELECTION = "Selection";

        public static string UI = "UI";

        public static string SCENE_LOAD = "SceneLoad";

        public static string CAMERA = "Camera";

        //Here is the list of queues that are setup

        public static List<string> IDList = new List<string>()
        {
            GAMESTATE,
            DIALOGUE,
            SELECTION,
            UI,
            SCENE_LOAD,
            CAMERA
        };
    }
}
