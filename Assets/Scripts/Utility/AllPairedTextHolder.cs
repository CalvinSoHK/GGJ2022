using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.MessageQueue;

public class AllPairedTextHolder : MonoBehaviour
{

    int index = 0;
    string[] pair1 = new string[] {
            "This person places a divider at the grocery store checkout.",
            "This person takes their shoes off at home.",
            "This person uses a reusable straw.",
            "This person watches shows/movies with subtitles.",
            "This person brushes their teeth after every meal.",
            "This person doesn’t talk during the movies.",
            "This person shares their Netflix account.",
            "This person is the designated driver.",
            "This person sorts their plastic bottles from aluminum cans for recycling.",
            "This person adds indie games to their steam wishlist.",
            "This person returns unwanted shopping items back to where they found them.",
            "This person speaks in TikTok lingo.",
            "This person often speaks in a fake british accent.",
            "This person uses the term waifu/husbando.",
            "This person leaves their mouse on the screen when playing a video on zoom.",
            "This person spends money on pay to win microtransactions.",
            "This person wears their underwear inside out instead of washing.",
            "This person takes their shoes off in the office.",
            "This person listens to music in public places without headphones.",
            "This person asks “where’s my hug?”.",
            "This person asks couples “when’s the wedding?”.",
            "This person asks “Where are you actually from?”.",
            "This person refunds indie games when finishing them under 2 hours.",
            "This person rages at strangers in online video games.",
            "This person spends time paying at the grocery store in pennies.",
            "This person sets 10+ alarms to wake up in the morning.",
            "This person picks up their pets to cuddle when they clearly don’t want to.",
            "This person insists crypto is the future.",
        };

    string[] pair2 = new string[] {
            "This person let’s someone with few items pass them in the grocery line.",
            "This person changes out of their outside clothe at home.",
            "This person uses a Thermos.",
            "This person watches dubbed anime.",
            "This person washes their hands consistently.",
            "This person cleans up after themselves at the movies.",
            "This person calls you an Uber after a night out.",
            "This person will carpool as often as possible.",
            "This person folds their cardboard boxes for recycling.",
            "This person buys games on itch.io.",
            "This person folds unwanted clothes back in the correct spot at the store.",
            "This person talks in Twitch memes.",
            "This person pronounces Barcelona with a lisp.",
            "This person watches anime in english dub.",
            "This person doesn’t mute during zoom calls when not speaking.",
            "This person pre-orders games before any information comes out about them.",
            "This person doesn’t wash their hands after going number one.",
            "This person reclines their seat on the plane.",
            "This person holds the door open for strangers at awkward distances.",
            "This person comments “thanks for the invite” when previous shared experiences are brought up.",
            "This person asks couples “when are you having a baby?”.",
            "This person compliments your ability to speak english.",
            "This person believes shorter games shouldn’t cost $60.",
            "This person abandons their team during multiplayer games.",
            "This person is unprepared to order at register after being in line for a while.",
            "This person snoozes their alarm for an inordinate amount of time.",
            "This person interacts with stranger’s pets before asking for permission.",
            "This person buys NFTs.",
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
