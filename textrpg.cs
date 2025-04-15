using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
class Solution
{

    // 이름 입력 
    static string WriteName()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        Console.WriteLine("원하시는 이름을 설정해주세요 : \n");
        string s = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("입력하신 이름은 : " + s + " 입니다.\n");
        return s;
    }
    // 이름 저장
    static string CheckName()
    {
        while (true)
        {
            Console.Clear();
            string name = WriteName();
            Console.WriteLine("1. 저장 \n2. 취소 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            int n = int.Parse(Console.ReadLine());

            if (n == 1)
            {
                Console.Clear();
                return name;
            }
            else if (n == 2)
            {
                Console.WriteLine();
                continue;
            }
            Console.WriteLine("1과 2 중 하나를 입력해주세요.\n");
        }
    }

    // 직업 선택 정보 enum
    enum Job
    {
        전사 = 1, 도적 = 2
    }

    static Job SelectJob()
    {
        Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
        while (true)
        {
            Console.WriteLine("원하시는 직업을 선택해주세요.");
            Console.WriteLine();

            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            int n = int.Parse(Console.ReadLine());

            if (n == 1 || n == 2)
                return (Job)n;
            Console.WriteLine("1과 2 중 하나를 입력해주세요.");
            Console.WriteLine();
        }
    }

    // 메인 화면 탭 관리
    static void GameStart(Player player, List<Item> items, List<Item> inventory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 상태보기 \n2. 인벤토리 \n3. 상점\n4. 던전 입장\n5. 휴식하기\n6. 종료하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 6)
            {
                return;
            }
            if (input == 1)
                PlayerInfo(player, items, inventory);
            else if (input == 2)
            {
                GoInventory(player, items, inventory);
            }
            else if (input == 3)
            {
                Goshop(player, items, inventory);
            }
            else if (input == 4)
            {
                GoDungeon(player, items, inventory);
            }
            else if (input == 5)
            {
                Rest(player, items, inventory);
            }
            else
            {
                Console.WriteLine("다시 입력해주세요.");
            }
        }
    }
    // 플레이어 정보 클래스
    public class Player
    {
        public string name { get; set; }
        public string job { get; set; }
        public int level { get; set; }
        public int attack { get; set; }
        public int shield { get; set; }
        public int health { get; set; }
        public int gold { get; set; }
        public Player() { }
        public Player(string name, string job, int level, int attack, int shield, int health, int gold)
        {
            this.name = name;
            this.job = job;
            this.level = level;
            this.attack = attack;
            this.shield = shield;
            this.health = health;
            this.gold = gold;
        }

    }
    // 방어구 타입과 스텟 정보
    public class Effect
    {
        public string type { get; set; }
        public int value { get; set; }

        public Effect() { }

        public Effect(string type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
    // 플레이어 상태탭 관리
    static void PlayerInfo(Player player, List<Item> items, List<Item> inventory)
    {
        Console.Clear();
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

        Console.WriteLine($"Lv. {player.level}");
        Console.WriteLine($"{player.name} ( {player.job} )");
        Console.WriteLine($"공격력 : {player.attack}");
        Console.WriteLine($"방어력 : {player.shield}");
        Console.WriteLine($"체 력 : {player.health}");
        Console.WriteLine($"Gold : {player.gold}G\n");

        Console.WriteLine($"0. 나가기\n");
        while (true)
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int n = int.Parse(Console.ReadLine());
            if (n == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("0을 입력하셔야 나가실 수 있습니다.");
            }
        }
    }
    public class Item
    {
        public bool isEquipped { get; set; }
        public string name { get; set; }
        public Effect effect { get; set; }
        public string info { get; set; }
        public int price { get; set; }
        public bool isHave { get; set; }
        public Item() { }
        public Item(string name,Effect effect, string info, int price, bool isHave, bool isEquipped)
        {
            this.name = name;
            this.effect = effect;
            this.info = info;
            this.price = price;
            this.isHave = isHave;
            this.isEquipped = isEquipped;
        }
    }
    // 아이템들 인벤토리탭에서 정보
    static void InventoryInfo(List<Item> inventory)
    {
        Console.WriteLine("[아이템 목록]");
        foreach (var item in inventory)
        {
            if (item.isEquipped)
                Console.WriteLine($"[E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
            else
                Console.WriteLine($"{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
        }
        Console.WriteLine();
    }
    // 아이템들 장착여부 정보
    static void EquippedInfo(List<Item> inventory)
    {
        int idx = 1;
        Console.WriteLine("[아이템 목록]");
        foreach (var item in inventory)
        {
            if (item.isEquipped)
                Console.WriteLine($"- {idx} [E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
            else
                Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info}");
            idx++;
        }
    }
    // 아이템들 상점에서 정보
    static void ShopInfo(List<Item> items, List<Item> inventory)
    {
        Console.WriteLine("[아이템 목록]");
        int idx = 1;
        foreach (var item in items)
        {
            bool isHaved = inventory.Any(x => x.name == item.name);
            if (isHaved)
            {
                Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | 구매완료");
            }
            else
                Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price} G");
            idx++;
        }
        Console.WriteLine();
    }
    // 아이템들 판매시 정보 
    static void SellInfo(List<Item> inventory)
    {
        int idx = 1;
        Console.WriteLine("[아이템 목록]");
        foreach (var item in inventory)
        {
            if (item.isEquipped)
                Console.WriteLine($"- {idx} [E]{item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price}");
            else
                Console.WriteLine($"- {idx} {item.name.PadRight(10)}| {item.effect.type} + {item.effect.value} | {item.info} | {item.price}");
            idx++;
        }
    }
    // 인벤토리탭 관리
    static void GoInventory(Player player, List<Item> items, List<Item> inventory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n");

            InventoryInfo(inventory);

            Console.WriteLine("1.장착 관리 \n2.나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                EquippedManage(player, items, inventory);
            }
            else if (input == 2)
            {
                return;
            }
            else
            {
                Console.WriteLine("다시 입력해주세요.");
            }
        }
    }
    // 장착탭 관리
    static void EquippedManage(Player player, List<Item> items, List<Item> inventory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.\n");
            EquippedInfo(inventory);


            Console.WriteLine("\n0. 나가기 \n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            int index;
            if (input == 0)
            {
                return;
            }
            else if (inventory[input - 1].isHave)
            {
                if (IsSameEffectEquipped(inventory, inventory[input - 1].effect.type, out index))
                {
                    inventory[index].isEquipped = false;
                    inventory[input - 1].isEquipped = true;
                    if (inventory[input - 1].effect.type == "방어력")
                    {
                        player.shield -= inventory[index].effect.value;
                        player.shield += inventory[input - 1].effect.value;
                    }
                    else
                    {
                        player.attack -= inventory[index].effect.value;
                        player.attack += inventory[input - 1].effect.value;
                    }
                    Console.WriteLine($"{inventory[index].name}을 장착 해제하고 {inventory[input - 1].name} 장착하였습니다.\n");
                }
                else
                {
                    inventory[input - 1].isEquipped = true;
                    if (inventory[input - 1].effect.type == "방어력")
                    {
                        player.shield += inventory[input - 1].effect.value;
                    }
                    else
                    {
                        player.attack += inventory[input - 1].effect.value;
                    }
                    Console.WriteLine($"{inventory[input - 1].name}을 장착하였습니다.\n");
                }
            }
            else if (inventory[input-1].isHave)
            {
                Console.WriteLine("아이템을 가지고 있지 않습니다. 다시 입력해주세요 \n");
            }
        }

    }
    // 아이템 장착하고 있었는지 확인
    static bool IsSameEffectEquipped(List<Item> inventory, string type, out int index)
    {
        foreach (var item in inventory)
        {
            if (item.isEquipped && item.effect.type == type)
            {
                index = inventory.IndexOf(item);
                return true; // 이미 같은 종류 장착 중
            }
        }
        index = -1;
        return false;
    }
    // 상점탭 관리
    static void Goshop(Player player, List<Item> items, List<Item> inventory)
    {
        int idx = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(idx);
            idx++;
            Console.WriteLine("상점\n 필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            ShopInfo(items, inventory);
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매 \n2. 아이템 판매\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                return;
            }
            else if (input == 1)
            {
                ItemBuy(player, items, inventory);
            }
            else if (input == 2)
            {
                ItemSell(player, items, inventory);
            }
            else
            {
                Console.WriteLine("다시 입력해주세요.");
            }
        }
    }
    // 아이템 판매하고 UI 갱신
    static void UpdateSellUI(Player player, List<Item> items, List<Item> inventory)
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("필요한 아이템을 판매할 수 있는 상점입니다.\n");

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G\n");

        SellInfo(inventory);
        Console.WriteLine();
        Console.WriteLine("0. 나가기\n");

    }
    // 아이템 판매 관리
    static void ItemSell(Player player, List<Item> items, List<Item> inventory)
    {

        while (true)
        {
            UpdateSellUI(player, items, inventory);
            Console.WriteLine("판매하고 싶은 아이템 번호를 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
            {
                return;
            }
            if (input > inventory.Count || input < 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                continue;
            }
            if (inventory[input - 1].isEquipped)
            {
                inventory[input - 1].isEquipped = false;
            }
            double sellPrice = inventory[input - 1].price * 0.85;
            //Console.WriteLine($"{inventory[input-1].name}이 {sellPrice:n1}가격에 팔렸습니다.");
            player.gold += (int)sellPrice;
            inventory.RemoveAt(input - 1);
            UpdateSellUI(player, items, inventory);
        }

    }
    // 아이템 구매하고 UI 갱신
    static void UpdateBuyUI(Player player, List<Item> items, List<Item> inventory)
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{player.gold} G\n");

        ShopInfo(items, inventory);
        Console.WriteLine();
        Console.WriteLine("0. 나가기\n");
    }
    // 아이템 구매 관리
    static void ItemBuy(Player player, List<Item> items, List<Item> inventory)
    {
        while (true)
        {
            UpdateBuyUI(player, items, inventory);
            Console.WriteLine("구매하고 싶은 아이템 번호를 입력해주세요.");
            int input = int.Parse(Console.ReadLine());

            if (input == 0)
            {
                return;
            }
            if (input > items.Count || input < 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                continue;
            }

            bool isHaved = inventory.Any(x => x.name == items[input - 1].name);
            if (isHaved)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                continue;
            }
            else
            {

                if (items[input - 1].price > player.gold)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    continue;
                }
                else
                {
                    player.gold -= items[input - 1].price;
                    inventory.Add(items[input - 1]);
                    UpdateBuyUI(player, items, inventory);
                }
            }

        }

    }
    // 휴식 탭 관리
    static void Rest(Player player, List<Item> items, List<Item> inventory)
    {
        while (true)
        {
            UdateRestUI(player);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());

            if (input < 0 || input > 1)
            {
                Console.WriteLine("다시 입력해주세요");
                continue;
            }
            if (input == 0)
            {
                return;
            }
            if (input == 1)
            {
                if (player.gold > 500)
                {
                    player.health = 100;
                    player.gold -= 500;
                    Console.WriteLine("휴식을 완료했습니다.\n");
                    UdateRestUI(player);
                }
                else
                {
                    Console.WriteLine("Gold가 부족합니다.\n");
                }
            }
        }
        // 휴식 탭 UI 업데이트
        static void UdateRestUI(Player player)
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. ( 보유 골드 : {player.gold} )\n");

            Console.WriteLine("1. 휴식하기 \n0. 나가기\n");
        }

    }
    // 던전 탭 관리
    static void GoDungeon(Player player, List<Item> items, List<Item> inventory)
    { 
        while (true)
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
            Console.WriteLine("0.나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input < 0 || input > 3)
            {
                Console.WriteLine("다시 입력해주세요.");
            }
            Random rand = new Random();
            int orgGold = player.gold;
            int orgHealth = player.health;
            if (input == 0)
            {
                return;
            }
            if (input == 1)
            {
                ResultDungeon(player, items, inventory, BattleDungeon(player, 5, 1000, rand), "쉬움", orgGold, orgHealth);

            }
            else if (input == 2)
            {
                ResultDungeon(player, items, inventory, BattleDungeon(player, 11, 1700, rand), "쉬움", orgGold, orgHealth);

            }
            else if (input == 3)
            {
                ResultDungeon(player, items, inventory, BattleDungeon(player, 17, 2500, rand), "쉬움", orgGold, orgHealth);
            }

        }


    }
    // 던전 결과 여부 확인
    static bool BattleDungeon(Player player, int shieldLevel, int successGold, Random rand)
    {
        if (player.shield < shieldLevel && rand.Next(100) < 40)
        {
            player.health /= 2;
            return false;
        }
        else
        {
            int dif = player.shield - shieldLevel;
            int heal = rand.Next(20 + dif, 36 + dif); // 35 포함
            double bonusRate = rand.Next(player.attack, player.attack * 2) / 100;
            int reward = successGold + successGold * (int)bonusRate;

            player.health -= heal;
            player.gold += reward;
            return true;
        }
    }
    // 던전 결과
    static void ResultDungeon(Player player, List<Item> items, List<Item> inventory, bool isSuccess, string level, int orgGold, int orgHealth)
    {
        
        while (true)
        {
            Console.Clear();
            if (isSuccess)
                Console.WriteLine("던전 클리어!");
            else
                Console.WriteLine("던전 실패!");
            Console.WriteLine($"{level} 던전을 {(isSuccess ? "클리어" : "실패")} 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {orgHealth} -> {player.health}");
            Console.WriteLine($"Gold {orgGold} -> {player.gold}");

            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = int.Parse(Console.ReadLine());
            if (input == 0)
                return;
            else
            {
                Console.WriteLine("다시 입력해주세요");
                continue;
            }
        }
    }
    // 데이터 저장
    public class GameSaveState
    {
        public Player player { get; set; }
        public List<Item> inventory { get; set; }
        public List<Item> items { get; set; }

        public GameSaveState() { }

        public GameSaveState(Player player, List<Item> inventory, List<Item> items)
        {
            this.player = player;
            this.inventory = inventory;
            this.items = items;
        }
    }
    static void Main()
    {
        Player player;
        // 빈 인벤토리 만들기
        List<Item> inventory = new List<Item>();
        // 상점에 아이템들 추가
        List<Item> items = new List<Item>();

        items.Add(new Item("수련자 갑옷", new Effect("방어력", 5), "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
        items.Add(new Item("무쇠 갑옷", new Effect("방어력", 9), "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500, false, false));
        items.Add(new Item("스파르타 갑옷", new Effect("방어력", 15), "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
        items.Add(new Item("낡은 검 ", new Effect("공격력", 2), "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, false));
        items.Add(new Item("청동 도끼", new Effect("공격력", 5), "어디선가 사용됐던거 같은 도끼입니다.", 1500, false, false));
        items.Add(new Item("스파르타의 창", new Effect("공격력", 7), "스파르타의 전사들이 사용했다는 전설의 창입니다.", 4000, false, false));

        // 그 전에 플레이어 정보가 있었을 경우
        if (File.Exists("player.json"))
        {
            // 파일 읽고 역직렬화
            string json = File.ReadAllText("player.json", Encoding.UTF8);
            GameSaveState saveState = JsonSerializer.Deserialize<GameSaveState>(json);

            // 객체에 할당
            player = saveState.player;
            inventory = saveState.inventory;
            items = saveState.items;

            Console.WriteLine("[불러오기 성공]");
        }
        else // 플레이어 정보가 없었을 경우
        {
            string playerName = CheckName();
            Job playerJob = SelectJob();
            player = new Player(playerName, playerJob.ToString(), 01, 10, 5, 100, 1500);
        }
        

        // 게임 시작
        GameStart(player, items, inventory);

        try
        {
            // 직렬화
            GameSaveState saveState = new GameSaveState(player, inventory, items);
            string saveJson = JsonSerializer.Serialize(saveState, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("player.json",  saveJson, Encoding.UTF8);
            Console.WriteLine("[저장 완료] player.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[저장 실패] {ex.Message}");
        }
    }
}
