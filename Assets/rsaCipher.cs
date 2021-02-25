﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KModkit;
using System;
using System.Linq;

public class rsaCipher : MonoBehaviour {
    
    public KMBombInfo Bomb;
    public KMBombModule module;
    public KMAudio Audio;
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
    public KMSelectable[] keyboard;
    public KMSelectable submit;
    public KMSelectable clear;
    public TextMesh[] screenTexts;
    public AudioClip solveAudio;
    private string submission;
    private string answer;
    List<string> wordList = new List<string>()
    {
                "ALUMNI", "AROUND", "ACROSS", "ALWAYS", "ACCESS", "ALMOST", "ACTION", "ACTUAL", "ANNUAL", "AMOUNT", "ANYONE", "ACTIVE", "ANSWER", "AGENCY", "APPEAR", "AFFECT", "ACCEPT", "ADVOCE", "APPEAL", "ATTACK", "AUTHOR", "ANIMAL", "ACTING", "ASSUME", "ASSIST", "ATTEND", "ANYWAY", "ASPECT", "AFFORD", "ARTIST", "ALPACA", "AFRAID", "AGENDA", "ARRIVE", "ADVISE", "ALLIED", "ABSENT", "ADJUST", "AUTUMN", "ACCENT", "ABSORB", "ASLEEP", "ANCHOR", "ATOMIC", "ATTACH", "ATTAIN", "ASSERT", "ABSURD", "ASSIGN", "ADMIRE", "ARCADE", "ARCHER", "ABRUPT", "AFFIRM", "ASHORE", "ACCUSE", "ANALOG", "ALMOND", "APATHY", "ASCEND",
                "BEFORE", "BETTER", "BECOME", "BEHIND", "BECAME", "BEYOND", "BUDGET", "BOTTOM", "BRANCH", "BOUGHT", "BATTLE", "BRIDGE", "BROKEN", "BANDIT", "BACKED", "BRIGHT", "BEHALF", "BEAUTY", "BAYOUS", "BORDER", "BREATH", "BOTTLE", "BELONG", "BUTTON", "BARELY", "BESIDE", "BREACH", "BITTER", "BOTHER", "BUTTER", "BAOBAB", "BUTLER", "BASKET", "BALLET", "BRONZE", "BARREL", "BORROW", "BEHAVE", "BUNDLE", "BANNER", "BANKER", "BOXING", "BREEZE", "BUBBLE", "BINARY", "BUCKET", "BOUNCE", "BROWSE", "BUFFET", "BANANA", "BOILER", "BEACON", "BEWARE", "BAKERY", "BOILED", "BUMPER", "BINDER", "BEAVER", "BADGER", "BAMBOO",
                "CHANGE", "COMMON", "COURSE", "COMING", "CREATE", "CHOICE", "CREDIT", "CHARGE", "CHANCE", "CLIENT", "CLOSED", "COUPLE", "CENTER", "CHOOSE", "CHOSEN", "CAUGHT", "COWBOY", "CORNER", "CLOSER", "COFFEE", "CUSTOM", "CIRCLE", "CAMERA", "COLUMN", "COPPER", "CASTLE", "COMPLY", "CARBON", "COSTLY", "CASUAL", "CARING", "COMEDY", "COTTON", "COMMIT", "CARPET", "CATTLE", "CLEVER", "CRUISE", "CONVEY", "COLLAR", "CANYON", "CHERRY", "COUPON", "CANVAS", "CEMENT", "CHORUS", "CANNON", "CALLER", "CIRCUS", "CANDLE", "COOLER", "COOLED", "CRUNCH", "CEREAL", "CLOSET", "CELLAR", "COSMIC", "CATBOY",
                "DESIGN", "DEMAND", "DIRECT", "DEGREE", "DOUBLE", "DAMAGE", "DEVICE", "DETAIL", "DOCTOR", "DECIDE", "DESIRE", "DEPEND", "DANGER", "DEFINE", "DEVILS", "DEALER", "DEFEAT", "DESERT", "DEFEND", "DETECT", "DECENT", "DIVINE", "DENIAL", "DRAGON", "DONATE", "DRAWER", "DELETE", "DEPART", "DOMINO", "DONKEY", "DELUXE", "DIALOG", "DECEIT", "DEFUSE", "DEDUCE", "DEDUCT", "DEBRIS", "DIVERT", "DEMISE", "DOMAIN", "DEBATE", "DECADE", "DIFFER", "DIGEST", "DEVOTE", "DEVISE", "DISMAY",
                "EITHER", "ENOUGH", "EFFECT", "EXPECT", "ENERGY", "EASILY", "EXCEPT", "ENABLE", "EFFORT", "ENGINE", "EDITOR", "EXPAND", "EXPERT", "EXTEND", "ENDING", "EATING", "ESCAPE", "EXPORT", "EMPIRE", "ENGAGE", "ENTITY", "EXCUSE", "EXEMPT", "EXOTIC", "EVOLVE", "EXPOSE", "EXPIRE", "ESTEEM", "ENDURE", "ELDEST", "EMBARK", "ENCORE", "EDIBLE", "EMBLEM", "ENIGMA", "EXPEND", "EUREKA", "ERRAND", "ELIXIR", "EXHALE", "ENDEAR", "EQUATE", "EMBRYO", "ENZYME", "ENTIRE", "ESTATE",
                "FUTURE", "FAMILY", "FORMER", "FOURTH", "FIGURE", "FOLLOW", "FRIEND", "FACTOR", "FORCED", "FORMAL", "FOREST", "FAMOUS", "FACING", "FLIGHT", "FAIRLY", "FELLOW", "FINISH", "FORMAT", "FORGET", "FLYING", "FALLEN", "FOUGHT", "FINGER", "FABRIC", "FROZEN", "FILTER", "FARMER", "FLOWER", "FISHER", "FUSION", "FLAVOR", "FIERCE", "FREEZE", "FORGOT", "FOSSIL", "FINITE", "FINALE", "FADING", "FAULTY", "FOLDER", "FACADE", "FRENZY", "FALCON", "FRIDGE", "FUNGUS", "FORBID", "FIASCO", "FIDDLE", "FLUFFY", "FERRET", "FAUCET",
                "GROWTH", "GLOBAL", "GROUND", "GARDEN", "GOLDEN", "GATHER", "GLANCE", "GARAGE", "GENTLE", "GUITAR", "GENIUS", "GAMBLE", "GALAXY", "GRAVEL", "GAMING", "GALLON", "GARLIC", "GRADES", "GIFTED", "GOTTEN", "GINGER", "GROOVE", "GREASE", "GLOOMY", "GREEDY", "GRASSY", "GREASY", "GUTTER", "GOALIE", "GLIDER", "GIGGLE", "GALLOP", "GRUDGE", "GADGET", "GRUMPY", "GOBLIN", "GOBLET", "GLITCH", "GEYSER", "GAZEBO", "GROOVY", "GALORE", "GRIEVE", "GRANNY", "GOVERN", "GUILTY", "GENDER", "GOPHER",
                "HEALTH", "HAPPEN", "HANDLE", "HARDLY", "HARDER", "HIDDEN", "HEIGHT", "HONEST", "HUNGRY", "HEAVEN", "HORROR", "HUNTER", "HARBOR", "HYBRID", "HEATED", "HEATER", "HAMMER", "HAZARD", "HUNGER", "HOCKEY", "HOLLOW", "HUMBLE", "HOOKED", "HEROIC", "HELMET", "HASSLE", "HURDLE", "HOURLY", "HUMANE", "HINDER", "HOPPER", "HERBAL", "HALVES", "HELPER", "HELPED", "HANGAR", "HUMMER", "HARDEN", "HAMPER", "HELIUM", "HIATUS", "HERESY", "HUSTLE", "HORRID", "HOMELY", "HEALER", "HOOVES", "HICCUP", "HIJACK",
                "INCOME", "IMPACT", "INSIDE", "INDEED", "ISLAND", "INTEND", "INTENT", "INVEST", "IMPORT", "IGNORE", "INFORM", "IMMUNE", "INVITE", "INSIST", "INTACT", "INDOOR", "INSERT", "IRONIC", "INSURE", "INSECT", "INSULT", "INWARD", "INVENT", "INSANE", "INJECT", "INVADE", "INFECT", "IMPAIR", "IMPEDE", "IGNITE", "INJURE", "INJURY", "INDIGO", "INHALE", "INVERT", "IMPURE", "INFAMY", "INDENT", "ICEBOX", "ICICLE", "INFEST", "INDUCT", "INFUSE", "IMPART", "INDUCE", "IMPOSE", "ITSELF", "INFANT", "INVOKE",
                "JUNIOR", "JERSEY", "JACKET", "JUNGLE", "JUMPER", "JUMPED", "JARGON", "JAGUAR", "JOYFUL", "JUMBLE", "JOYOUS", "JIGSAW", "JUGGLE", "JINGLE", "JESTER", "JUICED", "JUICER", "JAILER", "JAILED", "JAILOR", "JIGGLE", "JETWAY", "JETLAG", "JOCKEY", "JUNKER", "JASPER", "JAUNTY", "JOINED", "JOVIAL", "JINGLY", "JIVING", "JINXED", "JINXES", "JAMMED", "JAMMER", "JANGLY", "JEWELS", "JOKERS", "JOKILY", "JOKING", "JOULES", "JOGGER", "JUDGER", "JUKING", "JURIES", "JURORS", "JUSTLY", "JUSTLE", "JUICES", "JACKED", "JACKER",
                "KNIGHT", "KIDNEY", "KEEPER", "KINDLY", "KETTLE", "KARATE", "KITTEN", "KICKER", "KICKED", "KEYPAD", "KINDLE", "KINGLY", "KAZOOS", "KELVIN", "KERNEL", "KENNEL", "KEENED", "KEENLY", "KEELED", "KEBOBS", "KNOCKS", "KRAKEN",
                "LITTLE", "LEADER", "LIKELY", "LIVING", "LATEST", "LETTER", "LEAGUE", "LISTEN", "LAUNCH", "LENGTH", "LEAVES", "LINKED", "LOSING", "LIGHTS", "LIQUID", "LEGACY", "LUXURY", "LAWYER", "LESSON", "LOVELY", "LESSER", "LOADED", "LINEAR", "LANDED", "LOCATE", "LAYOUT", "LOVING", "LEGEND", "LIVELY", "LOUNGE", "LONELY", "LATELY", "LADDER", "LEGION", "LOCKER", "LAPTOP", "LAWFUL", "LINGER", "LUMBER", "LOSSEN", "LAGOON", "LIZARD", "LOTION", "LOCALE", "LIVERY", "LOATHE", "LOADER", "LOCUST",
                "MANIAC", "MARKET", "MAKING", "MEMBER", "MATTER", "MIDDLE", "MOVING", "MANAGE", "MOMENT", "MODERN", "METHOD", "MINUTE", "MEMORY", "MASTER", "MANNER", "MYSELF", "MEDIUM", "MUSKET", "MAINLY", "MOTION", "MOBILE", "MARKED", "MUSEUM", "MOSTLY", "MUTUAL", "MARGIN", "MODULE", "MINING", "MANUAL", "MODEST", "MIRROR", "MATURE", "MUSCLE", "MATRIX", "MEDIAN", "MODIFY", "MORALE", "MARBLE", "MOTIVE", "MARKER", "METRIC", "MENTOR", "MAGNET", "MELODY", "MONKEY", "MEADOW", "MYSTIC", "MAYHEM", "MAKEUP", "MANTLE", "MAILED", "MAILER", "MOLTEN", "MEMOIR", "MIRAGE", "MUTANT", "MISLED", "MISUSE",
                "NUMBER", "NEARLY", "NATURE", "NATION", "NORMAL", "NOTICE", "NATIVE", "NOBODY", "NARROW", "NEARBY", "NIGHTS", "NEEDLE", "NOTIFY", "NOVICE", "NICKEL", "NOZZLE", "NIMBLE", "NAPKIN", "NEGATE", "NECTAR", "NUGGET", "NODDLE", "NIBBLE", "NOTATE", "NEATLY", "NICEST", "NINJAS", "NOBLES", "NOSIER", "NOSILY", "NOVELS", "NUDGER", "NUDGES", "NURSED", "NUANCE",
                "OFFICE", "OPTION", "OBTAIN", "OUTPUT", "ONLINE", "OBJECT", "ORANGE", "OFFSET", "ORIGIN", "OXYGEN", "OCCUPY", "OPPOSE", "OUTLET", "OUTFIT", "ORDEAL", "ONWARD", "OYSTER", "OUTLAW", "OUTAGE", "OBTUSE", "OUTWIT", "OCELOT", "OBEYED", "OCCURS", "OCTAVE", "OCTANE", "OCULAR", "OFFERS", "OLIVES", "OLDEST", "OMELET", "ONIONS", "OPENLY", "OUNCES", "OVERDO", "OVERLY", "OWNING",
                "PEOPLE", "PUBLIC", "PERIOD", "PLEASE", "POLICY", "PERSON", "POLICE", "PROFIT", "PLAYER", "PRETTY", "PARENT", "PROPER", "PICKED", "PLENTY", "PROVEN", "PURSUE", "PARTLY", "PREFER", "PRINCE", "POCKET", "PACKED", "PALACE", "PHRASE", "PLANET", "PACKET", "POETRY", "PORTAL", "POWDER", "POLISH", "PLASMA", "PROMPT", "PARADE", "PURPLE", "PEPPER", "POSTER", "PENCIL", "POTATO", "PURITY", "PUZZLE", "POLITE", "PICKUP", "POETIC", "PICNIC", "PARDON", "PLAQUE", "PILLOW", "PILLAR", "PASTRY", "PIGEON", "PEANUT",
                "QUARTZ", "QUARRY", "QUAINT", "QUIVER", "QUENCH", "QUEASY", "QUICHE", "QUOTES", "QUOTER", "QUALMS", "QUAILS", "QUAKES", "QUAKED", "QUARKS", "QUACKS", "QUEENS", "QUEUES", "QUEUED", "QUIRKS", "QUIRKY", "QUILLS",
                "REPORT", "RESULT", "REALLY", "RECENT", "RECORD", "RETURN", "RATHER", "REASON", "REVIEW", "REFORM", "REDUCE", "REMAIN", "REGION", "RAISED", "RELIEF", "RISING", "REMOTE", "RETAIN", "REGARD", "REMOVE", "RATING", "RELATE", "REPAIR", "RARELY", "RULING", "RESORT", "REPEAT", "ROBUST", "REVEAL", "REPLAY", "RECALL", "RANDOM", "REWARD", "RIDING", "RESCUE", "RUBBER", "REVISE", "REFUSE", "RESIST", "RETIRE", "RENTAL", "REMIND", "REJECT", "RHYTHM", "REMEDY", "RUNNER", "RECIPE", "RITUAL", "RIBBON", "ROCKET", "RABBIT", "RESIGN", "REMARK", "RADIUS", "REFUGE", "REFUND", "REPAID", "RIPPED", "ROSTER", "ROTARY", "REDEEM", "REVIVE", "RIDDEN", "RUNWAY", "REVOLT", "REFINE", "ROTTEN", "RECKON", "REPEAL", "RELISH", "ROTATE", "REVERT", "REFLEX", "RUBBLE", "REOPEN",
                "SHOULD", "SYSTEM", "SECOND", "SCHOOL", "STRONG", "SIZZLE", "SINGLE", "SOCIAL", "SERIES", "STREET", "SENIOR", "SIMPLY", "SOURCE", "SUPPLY", "SIMPLE", "SEASON", "SUMMER", "SAYING", "SAFETY", "SECTOR", "STATUS", "SIGNED", "SQUARE", "SECURE", "SURVEY", "SEARCH", "SPRING", "SCREEN", "STUDIO", "SPREAD", "SELECT", "SPEECH", "SYMBOL", "SPIRIT", "STABLE", "SOUGHT", "SAMPLE", "SCHEME", "SILVER", "SIGNAL", "STRIKE", "SEVERE", "SECRET", "SWITCH", "SAVING", "STEADY", "STRUCK", "STREAM", "SMOOTH", "SURELY", "SOLELY", "SUMMIT", "SUDDEN", "SLIGHT", "SPOKEN", "SILENT", "SETTLE", "STRICT", "SUBMIT", "STRING", "STOLEN", "SHADOW", "SINGER", "SOCCER", "SUPERB", "SERIAL", "SUBTLE", "SOONER", "STATIC", "SHIELD", "STANCE", "SCRIPT", "SACRED", "SIERRA", "SELDOM", "SALMON", "SHOWER", "SPHERE", "SPRINT", "SUNSET", "STRIVE", "STEREO", "SCARCE",
                "THOUGH", "TAKING", "TRYING", "TARGET", "TRAVEL", "THEORY", "TETRIS", "THANKS", "TOWARD", "TIMING", "TALENT", "TAUGHT", "TEAPOT", "TICKET", "TISSUE", "TENNIS", "TIMELY", "TENDER", "THROWN", "TACKLE", "TURKEY", "TRIPLE", "TEMPLE", "THROAT", "TIMBER", "TUNNEL", "TONGUE", "TRAGIC", "TROPHY", "TITLED", "THESIS", "TOILET", "THEIRS", "TAILOR", "THREAD", "THRIVE", "TOMATO", "THRILL", "TRICKY", "THRONE", "TACTIC", "TAXING", "TRENDY", "THIRST", "TUMBLE", "TURTLE", "TRIVIA", "TANGLE", "THWART", "TYCOON", "TESTER", "TRIPOD", "TINKER", "TUXEDO",
                "UNITED", "UNIQUE", "UNLESS", "USEFUL", "UNABLE", "UPDATE", "UNLIKE", "URGENT", "UNFAIR", "UPWARD", "UPTIME", "UNPAID", "UPLOAD", "UNUSED", "UPHELD", "UNREST", "UNEVEN", "UNLOCK", "UNSURE", "UTMOST", "UNEASY", "USABLE", "UNSAFE", "UNSEEN", "UPTAKE", "UPHOLD", "UNTRUE", "UNVEIL", "UNJUST", "UNISON", "UMPIRE", "UNFOLD", "UPSIDE", "UNREAL", "UNWISE", "UNLOAD", "UTOPIA", "UPROAR", "UNRULY", "UNWIND", "URCHIN", "UNWELL", "UPROOT", "UNWRAP",
                "VALLEY", "VOLUME", "VISION", "VISUAL", "VENDOR", "VERSUS", "VICTIM", "VARIED", "VIABLE", "VIRTUE", "VESSEL", "VERBAL", "VACUUM", "VACANT", "VIEWER", "VECTOR", "VERIFY", "VELVET", "VOYAGE", "VANITY", "VIOLIN", "VIOLET", "VANISH", "VEILED", "VORTEX", "VERSED", "VOLLEY", "VIGOUR", "VOODOO", "VERTEX", "VACATE", "VERMIN", "VAULTS", "VARIES", "VASTLY", "VALUED", "VALUES", "VALVES", "VEGGIE", "VEXING", "VIKING", "VIEWED",
                "WYVERN", "WITHIN", "WEIGHT", "WINDOW", "WINTER", "WEALTH", "WINNER", "WONDER", "WEEKLY", "WORKER", "WOODEN", "WISDOM", "WORTHY", "WARMTH", "WIRING", "WIZARD", "WALNUT", "WALLET", "WEAKEN", "WANDER", "WOLVES", "WAITER", "WEAKLY", "WORSEN", "WASHER", "WRENCH", "WREATH", "WITHER", "WAFFLE", "WEASEL", "WETTER", "WOBBLE", "WIGGLE", "WHIMSY", "WIDGET", "WELDER", "WOEFUL", "WADDLE", "WHEEZE", "WALRUS", "WEBBED", "WALKER", "WALKED", "WHEELS", "WORKED", "WRITES",
                "YELLOW", "YEARLY", "YOGURT", "YONDER", "YACHTS",
                "ZODIAC", "ZOMBIE", "ZIPPER", "ZIGZAG", "ZEALOT", "ZINGER", "ZAPPED", "ZAPPER", "ZIGGED", "ZAGGED", "ZOOMED", "ZEBRAS", "ZEROES"
    };
    //Entering this comment so I can change the build
    void Awake()
    {
        moduleId = moduleIdCounter++;
        moduleSolved = false;
        int[] cursors = {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25
        };
        string alpha = "QWERTYUIOPASDFGHJKLZXCVBNM";
        foreach (int i in cursors)
            keyboard[i].OnInteract = delegate () { KeyPress(alpha[i], keyboard[i]); return false; };
        submit.OnInteract = delegate () { Submit(); return false; };
        clear.OnInteract = delegate () { Clear(); return false; };
    }
    // Use this for initialization
    void Start ()
    {
        List<int> primes = new List<int>()
        {
            11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97
        };
        int p1 = primes[UnityEngine.Random.Range(0, primes.Count)];
        primes.Remove(p1);
        int p2 = primes[UnityEngine.Random.Range(0, primes.Count)];
        int n = p1 * p2;
        Debug.LogFormat("[RSA Cipher #{0}] Generated P1: {1}", moduleId, p1);
        Debug.LogFormat("[RSA Cipher #{0}] Generated P2: {1}", moduleId, p2);
        Debug.LogFormat("[RSA Cipher #{0}] Calculated N: {1} * {2} = {3}", moduleId, p1, p2, n);
        Debug.LogFormat("[RSA Cipher #{0}] Calculating GCD", moduleId);
        int lamn = ((p1 - 1) * (p2 - 1)) / GCD(p1 - 1, p2 - 1);
        Debug.LogFormat("[RSA Cipher #{0}] Calculated λ(N): (({1} - 1) * ({2} - 1)) / GCD({1} - 1, {2} - 1) = {3}", moduleId, p1, p2, lamn);
        primes.Clear();
        for(int aa = 2; aa < lamn; aa++)
        {
            if (GCD1(aa, lamn))
                primes.Add(aa);
        }
        int e = primes[UnityEngine.Random.Range(0, primes.Count)];
        Debug.LogFormat("[RSA Cipher #{0}] Generated E: {1}", moduleId, e);
        Debug.LogFormat("[RSA Cipher #{0}] Calculating D", moduleId);
        int t1 = 0;
        int t2 = 1;
        int a = lamn + 0;
        int b = e + 0;
        while(b > 0)
        {
            int q = a / b;
            int r = a % b;
            int t3 = t1 - (t2 * q);
            Debug.LogFormat("[RSA Cipher #{0}] A: {1}", moduleId, a);
            Debug.LogFormat("[RSA Cipher #{0}] B: {1}", moduleId, b);
            Debug.LogFormat("[RSA Cipher #{0}] Q: {1}", moduleId, q);
            Debug.LogFormat("[RSA Cipher #{0}] R: {1}", moduleId, r);
            Debug.LogFormat("[RSA Cipher #{0}] T1: {1}", moduleId, t1);
            Debug.LogFormat("[RSA Cipher #{0}] T2: {1}", moduleId, t2);
            Debug.LogFormat("[RSA Cipher #{0}] T3: {1}", moduleId, t3);
            t1 = t2 + 0;
            t2 = t3 + 0;
            a = b + 0;
            b = r + 0;
        }
        t2 = t1 + 0;
        while (t1 < 0)
            t1 += lamn;
        int d = t1 % lamn;
        Debug.LogFormat("[RSA Cipher #{0}] Calculated D: {1} % {2} = {3}", moduleId, t2, lamn, d);
        screenTexts[0].text = "N:" + n;
        screenTexts[1].text = "E:" + e;
        submission = "";
        answer = wordList[UnityEngine.Random.Range(0, wordList.Count)].ToUpperInvariant();
        int[] encrypt = new int[6];
        string alpha = "--ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for(int aa = 0; aa < 6; aa++)
        {
            b = alpha.IndexOf(answer[aa]);
            int c = 1;
            for(int bb = 0; bb < e; bb++)
                c = (c * b) % n;
            Debug.LogFormat("[RSA Cipher #{0}] {1} -> {2}^{3} % {4} = {5}", moduleId, answer[aa], b, e, n, c);
            encrypt[aa] = c + 0;
        }
        screenTexts[2].text = encrypt[0] + " " + encrypt[1] + " " + encrypt[2] + "\n" + encrypt[3] + " " + encrypt[4] + " " + encrypt[5];
        StartCoroutine(FlashingCursor());
    }
    IEnumerator FlashingCursor()
    {
        while (true)
        {
            screenTexts[3].text = submission + "▮";
            yield return new WaitForSeconds(1.0f);
            screenTexts[3].text = submission;
            yield return new WaitForSeconds(1.0f);
        }
    }
    int GCD(int a, int b)
    {
        int num;
        int remainder;
        if (a > b)
        {
            num = b + 0;
            remainder = a % b;
            Debug.LogFormat("[RSA Cipher #{0}] {1} % {2} = {3}", moduleId, a, b, remainder);
        }
        else
        {
            num = a + 0;
            remainder = b % a;
            Debug.LogFormat("[RSA Cipher #{0}] {1} % {2} = {3}", moduleId, b, a, remainder);
        }
        while (remainder > 0)
        {
            int prev = remainder + 0;
            remainder = num % remainder;
            Debug.LogFormat("[RSA Cipher #{0}] {1} % {2} = {3}", moduleId, num, prev, remainder);
            num = prev + 0;
        }
        Debug.LogFormat("[RSA Cipher #{0}] GCD({1}, {2}) = {3}", moduleId, a, b, num);
        return num;
    }
    bool GCD1(int a, int b)
    {
        for(int i = 2; i <= a && i <= b; i++)
        {
            if (a % i == 0 && b % i == 0)
                return false;
        }
        return true;
    }
    void KeyPress(char l, KMSelectable key)
    {
        if(!(moduleSolved))
        {
            key.AddInteractionPunch();
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            StopAllCoroutines();
            if (submission.Length < 6)
                submission = submission + "" + l;
            StartCoroutine(FlashingCursor());
        }
    }
    void Submit()
    {
        if (!(moduleSolved))
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            submit.AddInteractionPunch();
            if (answer.Equals(submission))
            {
                StopAllCoroutines();
                screenTexts[0].text = "MODULE";
                screenTexts[1].text = "SOLVED";
                screenTexts[2].text = "";
                screenTexts[3].text = "";
                Audio.PlaySoundAtTransform(solveAudio.name, transform);
                moduleSolved = true;
                module.HandlePass();
            }
            else
            {
                module.HandleStrike();
                StopAllCoroutines();
                submission = "";
                StartCoroutine(FlashingCursor());
            }
        }
    }
    void Clear()
    {
        if (!(moduleSolved))
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            clear.AddInteractionPunch();
            StopAllCoroutines();
            submission = "";
            StartCoroutine(FlashingCursor());
        }
    }
#pragma warning disable 414
    private string TwitchHelpMessage = "Submit the decrypted word with !{0} submit QWERTY";
#pragma warning restore 414
    private IEnumerator ProcessTwitchCommand(string command)
    {
        string[] split = command.ToUpperInvariant().Split(' ');
        if (split.Length != 2 || !split[0].Equals("SUBMIT") || split[1].Length != 6)
        {
            yield break;
        }
        bool flag = false;
        foreach(char c in split[1])
        {
            if("QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(c) < 0)
            {
                flag = true;
                break;
            }
        }
        if (flag)
        {
            yield break;
        }
        yield return null;
        yield return new WaitForSeconds(0.1f);
        foreach (char let in split[1])
        {
            keyboard["QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(let)].OnInteract.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        submit.OnInteract.Invoke();
        yield return new WaitForSeconds(0.1f);
        yield break;
    }
}
