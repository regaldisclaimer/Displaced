using UnityEngine;
using System.Collections;

public class AutoLobbyDialogScript : MonoBehaviour {

	public string[] dialogGroup;

	private GameManagerSingleton gameManager;
	private string dialogGroupID;

	void Awake () {
		gameManager = GameManagerSingleton.Instance;
		dialogGroupID = gameManager.getConversationID();
	}

	// Use this for initialization
	void Start () {
		gameManager.setDialogString(getText());
		GameObject theText = GameObject.Find("DialogText");
		DialogTextScript theScript = theText.GetComponent<DialogTextScript>();
		theScript.fakeInstantiate();
	}


	// Update is called once per frame
	void Update () {

	}

	private string[] getText () {
		if (dialogGroupID == "IntroSequence1") {
			dialogGroup = new string[8] {
				"m Wait, this isn't my bed.",
				"m Why are the lights all red?",
				"m And why am I moving at such high speed?",
				"m Last I remember, I was dozing off on the couch",
				"m with a pint of Bon and Joorie's ice cream",
				"m watching Jennifer Aniston try to revive her career.",
				"m But this - this isn't my room...",
				"m have I... have I been kidnapped?"
			};
		} else if (dialogGroupID == "LobbySequence1") {
			dialogGroup = new string[13] {
				"l Alright, I found Xanthan Reynolds for you.",
				"s Uhh... no you didn't!",
				"f Yeah - this bozo is a lot flabbier.",
				"s And uglier!",
				"l Well, you told me to grab Xanthan Reynolds",
				"l from the Nebular system. Here he is.",
				"l If you're lucky, maybe this Xanthan will die quickly",
				"l *click*",
				"f How the hell is this supposed to help us finish these challenges?",
				"f We're never going to go home now!",
				"s Well let's put this chunk of blubber to the test.",
				"s Hey new meat - why don't you come run the bunny slope with us?",
				"s Let's see if you're any good..."
			};
		} else if (dialogGroupID == "LobbySequence2") {
			dialogGroup = new string[12] {
				"s Well, shoot.",
				"l Unfortunately, due to your collective poor performance on the bunny slope,",
				"l each of you have lost your shower privileges for the night.",
				"f Just let us switch him out! It can't be fun for you guys to watch this buffoon!",
				"l Actually, you have no idea how much the audience loved watching him.",
				"l It's hilarious! See how he does on a real challenge this time.",
				"s You totally grabbed this guy on purpose!",
				"l Give him a real challenge this time.",
				"l *click*",
				"f Hey, let's bring him to a level with a Blorb in it.",
				"s Aw man, that level is filthy...",
				"f Yeah, but maybe we can lose him in it..."
			};
		} else if (dialogGroupID == "LobbySequence3") {
			dialogGroup = new string[12] {
				"l Oh good, it looks like we can call off the search and rescue team,",
				"l now that you finally showed up.",
				"f Aww don't come near me. You smell like a trash can.",
				"s Looks like you gained a little weight there, bub.",
				"s The last thing we need for you to do is become slower.",
				"f Please go freshen up before my nose starts to bleed.",
				"s Hurry up though, we have been waiting here for ever",
				"s and we need to get back out there.",
				"f Let's see if this tortoise can keep up.",
				"f in the next arena I have planned for us.",
				"l Play nice boys, the viewers need him.",
				"l *click*"
			};
		} else if (dialogGroupID == "LobbySequence4") {
			dialogGroup = new string[8] {
				"s See? We could have finished another arena by the time he got back!",
				"f I'm surprised his chubby little arms didn't fall off climbing all those ladders",
				"s Maybe we need to start giving him some real challenges?",
				"l May I suggest trying your luck in the jungle?",
				"l Viewers really love that arena.",
				"f Man, that level is sketchy.",
				"f I never feel safe running through it...",
				"s Hey - nothing's broken on us yet in there..."
			};
		} else if (dialogGroupID == "LobbySequence5") {
			dialogGroup = new string[19] {
				"l Oh, there you are.",
				"l It's too bad that took so long -",
				"l we had the doctor on site for check ups,",
				"l but she left a while ago.",
				"l *click*",
				"f Awe, looks like the little blob learned some basic hygiene.",
				"f That's so adorable.",
				"f I can stand within fifteen feet of you now!",
				"s Well, since he made it this far,",
				"s maybe he isn't worthless after all.",
				"f HAHAHA!",
				"f Yeah right",
				"s No no, let's try his wits on this next level.",
				"s If he can get through it without getting lost,",
				"s we may be able to use him after all.",
				"f The only thing we will ever be able to use him for",
				"f is a trash can.",
				"f But fine - I'll go watch him give another pathetic attempt.",
				"f At least it'll provide me with some afternoon entertainment."
			};
		} else if (dialogGroupID == "LobbySequence6") {
			dialogGroup = new string[12] {
				"s Hey, wake up! He made it back!",
				"f No way his chunky butt made it over that huge gap at the end.",
				"s Yeah - I've seen dudes more athletic than this nugget miss that jump.",
				"l It's Sormsday. Time to give 'The Unbeatable Vault' another go.",
				"s Already? C'mon...you know that level isn't fair!",
				"l *click*",
				"f Plus there is no way we can complete the level",
				"f with this trash-vac weighing us down.",
				"s Hey, he's made it this far.",
				"s Maybe he can help us wear down the beam at least.",
				"f Whatever. I can't wait to beat this level.",
				"f Don't let me down Xanthan."
			};
		} else if (dialogGroupID == "LobbySequence7") {
			dialogGroup = new string[20] {
				"s I can't believe we actually completed the Unbeatable Vault!",
				"f I knew we would finally figure out how to beat it.",
				"s That wasn't us, that was all Xanthan.",
				"s I told you he wasn't worthless after all.",
				"f Pssssh - I would have passed it on my own,",
				"f even if he wasn't with us.",
				"s Oh yeah, why don't you go run it again then?",
				"s Show us.",
				"f Um, you know I am actually kind of tired.",
				"f I think I am just going to get a good night's rest instead.",
				"s Man you're pathetic.",
				"s You should really learn when to swallow your pride.",
				"s Thank you Xanthan. We wouldn't have been able",
				"s to complete this without you.",
				"l Congratulations contestants!",
				"l I can't believe somebody actually beat the Unbeatable Vault.",
				"l I also can't believe that it was this guy, of all people.",
				"l Also, it turns out that the viewers find it much more amusing",
				"l when you fail. So show's cancelled now.",
				"l You're all going home first thing in the morning."
			};
		} else if (dialogGroupID == "Level1Instruction1") {
			dialogGroup = new string[1] {
				"i Climb ladders with analog stick"
			};
		} else if (dialogGroupID == "DeathSequence1") {
			dialogGroup = new string[6] {
				"l Really? You couldn't beat that part?",
				"l Well, good news for you",
				"l Your pathetic nature has shot our ratings through the roof!",
				"l The network can't afford for you to die yet, so we brought you back",
				"l Give it another go, but don't make this a habit.",
				"l These treatments ain't cheap you know"
			};
		} else if (true) {
			dialogGroup = new string[8] {
				"m 222You can move up and down the ladder with the analog stick",
				"l 333 LOL",
				"i 444 more tests",
				"s 555 lsat one",
				"f sike more error",
				"m yeah",
				"l lol",
				"s senpai plz"
			};
			//add others
		}

		return dialogGroup;
	}
}
