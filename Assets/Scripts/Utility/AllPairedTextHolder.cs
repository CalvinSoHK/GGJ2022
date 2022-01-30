using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

public class AllPairedTextHolder : MonoBehaviour
{

    int index = 0;
    string[] pair1 = new string[] {
            "This person places a divider at the grocery checkout",
            "This person takes their shoes off at home",
            "Uses reusable straw",
            "Watches shows/movies with subtitles",
            "Brushes teeth after every meal",
            "Doesn’t talk during the movies",
            "Shares their Netflix account",
            "Is the designated driver",
            "Sorts their plastic bottles from aluminum cans for recycling",
            "Adds indie games to their steam wishlist",
            "Returns unwanted shopping items back to where they found them",
            "Speaks in TikTok lingo",
            "Speaks in fake british accent",
            "Uses the term waifu/husbando",
            "Leaves mouse on screen when playing a video on zoom",
            "Spends money on pay to win microtransactions",
            "Wears their underwear inside out instead of washing",
            "Takes their shoes off in the office",
            "Listens to music in public places without headphones",
            "Asks “where’s my hug?”",
            "Asks couples “when’s the wedding?”",
            "Asks “Where are you actually from?”",
            "Refunds indie games when finishing them under 2 hours",
            "Rages at strangers in online video games",
            "Spends time paying at the grocery store in pennies",
            "Sets 10+ alarms to wake up in the morning",
            "Picks up their pets to cuddle when they clearly don’t want to",
            "Insists crypto is the future",
        };

    string[] pair2 = new string[] {
            "Let’s someone with few items pass them in the grocery line",
            "Change out of their outside clothe at home",
            "Uses a Thermos",
            "Watches Dub Anime",
            "Washes hands consistently",
            "Cleans up after themselves at the movies",
            "Calls you an Uber after a night out",
            "Will carpool as often as possible",
            "Folds their cardboard boxes for recycling",
            "Buys games on itch.io",
            "Folds unwanted clothes back in the correct spot at the store",
            "Talks in Twitch memes",
            "Pronounces Barcelona with a lisp",
            "Watches anime in english dub",
            "Doesn’t mute during zoom calls when not speaking",
            "Pre-orders games before any information comes out about them",
            "Doesn’t wash their hands after going number one",
            "Reclines their seat on the plane",
            "Holds the door open for strangers at awkward distances",
            "Comments “thanks for the invite” when previous shared experiences are brought up",
            "Asks couples “when are you having a baby”",
            "Compliments your ability to speak english",
            "Believes shorter games shouldn’t cost $60",
            "Abandons team during multiplayer game",
            "Is unprepared to order at register after being in line for a while",
            "Snoozes their alarm for an inordinate amount of time",
            "Interacts with stranger’s pets before asking for permission",
            "Buys NFT",
        };

    private List<string> finalWords = new List<string>
    {
        "I have my whole life ahead of me...",
        "Please I have a family!",
        "Do you seriously think the other guy is better than me?",
        "... Please ...",
        "Well I guess this is the end.",
        "I die with honor.",
        "I have no regrets.",
        "Think of my pets! How are they going to go on?",
        "I'll pay you any amount if you pick me!",
        "I'll have you know I'm well connected. I could help you out.",
        "You wouldn't actually pick... that guy would you?",
        "Sweetie, you should pick the other guy!",
        "Pick me and I'll split my NFT profits with you!"
    };

    private List<string> curFinalWords = new List<string>();

    List<string> chara1Lines = new List<string>();
    List<string> chara2Lines = new List<string>();

    public string returnNeededString(bool whichChara)
    {
        if(whichChara)
        {
            chara1Lines.Add(pair1[index]);
            return pair1[index];
        }
        else
        {
            chara2Lines.Add(pair2[index]);
            return pair2[index];
        }
    }

    public void setNewIndex()
    {
        index = Random.Range(0, pair1.Length - 1);
    }

    public int getNewIndex()
    {
        return index;
    }

    public List<string> getCharacterLines(bool whichChara)
    {
        if(whichChara)
        {
            return chara1Lines;
        }
        else
        {
            return chara2Lines;
        }
    }

    public List<string> getFirstCharaLines()
    {
        return chara1Lines;
    }

    public List<string> getSecondCharaLines()
    {
        return chara2Lines;
    }

    public string GetDeathLine()
    {
        int index = Random.Range(0, curFinalWords.Count);
        string returnVal = curFinalWords[index];
        curFinalWords.RemoveAt(index);
        return returnVal;
    }

    public void ResetDeathLines()
    {
        curFinalWords.Clear();
        foreach(string words in finalWords)
        {
            curFinalWords.Add(words);
        }
    }

    /// <summary>
    /// Helps with debugging
    /// </summary>
    private void Awake()
    {
        ResetDeathLines();
    }

    private void OnEnable()
    {
        MessageQueuesManager.MessagePopEvent += HandleMessage;
    }

    private void OnDisable()
    {
        MessageQueuesManager.MessagePopEvent -= HandleMessage;
    }

    private void HandleMessage(string id, string msg)
    {
        if(id.Equals(MessageQueueID.GAMESTATE))
        {
            if (msg.Equals("Start"))
            {
                ResetDeathLines();
            }
        }
    }
}
