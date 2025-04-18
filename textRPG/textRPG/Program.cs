using System;
using System.Collections.Generic;

namespace textRPG
{
    public class Player
    {
        public string Name;
        public string Class;
        public int Level = 01;
        public int BaseAttack;
        public int BaseDefense;
        public int Attack;
        public int Defense;
        public int HP;
        public int Gold = 1500;

        public List<InventoryItem> Inventory = new List<InventoryItem>();

        public void SetClass(string chosenClass)
        {
            Class = chosenClass;

            switch (chosenClass)
            {
                case "전사":
                    BaseAttack = 10;
                    BaseDefense = 5;
                    HP = 100;
                    break;
                case "도적":
                    BaseAttack = 10;
                    BaseDefense = 5;
                    HP = 100;
                    break;
                default:
                    BaseAttack = 10;
                    BaseDefense = 5;
                    HP = 100;
                    break;
            }

            UpdateStats();
        }

        public void UpdateStats()
        {
            Attack = BaseAttack;
            Defense = BaseDefense;

            foreach (var item in Inventory)
            {
                if (item.Equipped)
                {
                    if (item.Stat.Contains("공격력"))
                        Attack += int.Parse(item.Stat.Split('+')[1]);
                    else if (item.Stat.Contains("방어력"))
                        Defense += int.Parse(item.Stat.Split('+')[1]);
                }
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level:00}");
            Console.WriteLine($"Chad ( {Class} )");
            int bonusAtk = Attack - BaseAttack;
            int bonusDef = Defense - BaseDefense;
            Console.WriteLine($"공격력 : {BaseAttack} {(bonusAtk > 0 ? $"+({bonusAtk})" : "")}");
            Console.WriteLine($"방어력 : {BaseDefense} {(bonusDef > 0 ? $"+({bonusDef})" : "")}");
            Console.WriteLine($"체 력 : {HP}");
            Console.WriteLine($"Gold : {Gold} G");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");
        }
    }

    public class InventoryItem
    {
        public string Name { get; }
        public string Stat { get; }
        public string Description { get; }
        public bool Equipped { get; set; }

        public InventoryItem(string name, string stat, string description)
        {
            Name = name;
            Stat = stat;
            Description = description;
            Equipped = false;
        }

        public override string ToString()
        {
            string prefix = Equipped ? "[E]" : "   ";
            return $"{prefix}{Name,-15} | {Stat} | {Description}";
        }
    }

    public class ShopItem
    {
        public string Name { get; }
        public string Stat { get; }
        public string Description { get; }
        public int Price { get; }
        public bool Purchased { get; set; }

        public ShopItem(string name, string stat, string description, int price)
        {
            Name = name;
            Stat = stat;
            Description = description;
            Price = price;
            Purchased = false;
        }

        public string Display()
        {
            string priceItem = Purchased ? "구매완료" : $"{Price,6} G";
            return $"- {Name,-13} | {Stat,-8} | {Description,-35} | {priceItem}";
        }
    }

    internal class Program
    {
        static List<ShopItem> shopItems = new List<ShopItem>
        {
            new ShopItem("수련자 갑옷", "방어력 +5", "수련에 도움을 주는 갑옷입니다.", 200),
            new ShopItem("무쇠갑옷", "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다.", 400),
            new ShopItem("스파르타의 갑옷", "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 800),
            new ShopItem("낡은 검", "공격력 +2", "쉽게 볼 수 있는 낡은 검 입니다.", 200),
            new ShopItem("청동 도끼", "공격력 +5", "어디선가 사용됐던거 같은 도끼입니다.", 400),
            new ShopItem("스파르타의 창", "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 800),
        };

        static void Main(string[] args)
        {
            Player player = new Player();

            SetPlayerName(player);
            ChooseClass(player);
            EnterVillage(player);
        }

        static void SetPlayerName(Player player)
        {
            while (true)
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 이름을 설정해주세요.\n");
                player.Name = Console.ReadLine();

                Console.WriteLine($"입력하신 이름은 {player.Name} 입니다.\n");
                Console.WriteLine("1.저장");
                Console.WriteLine("2.취소");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string saveInput = Console.ReadLine();

                if (saveInput == "1")
                {
                    Console.Clear();
                    break;
                }
                else if (saveInput == "2")
                {
                    Console.WriteLine("처음으로 돌아갑니다.\n");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine("처음으로 돌아갑니다.\n");
                }
            }
        }

        static void ChooseClass(Player player)
        {
            while (true)
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
                Console.WriteLine("원하시는 직업을 선택해주세요.\n");
                Console.WriteLine("1.전사");
                Console.WriteLine("2.도적");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string classInput = Console.ReadLine();

                if (classInput == "1")
                {
                    player.SetClass("전사");
                    Console.Clear();
                    break;
                }
                else if (classInput == "2")
                {
                    player.SetClass("도적");
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                }
            }
        }

        static void EnterVillage(Player player)
        {
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string activeInput = Console.ReadLine();

                if (activeInput == "1")
                {
                    Console.Clear();
                    player.UpdateStats();
                    player.ShowStatus();

                    while (true)
                    {
                        string statusInput = Console.ReadLine();
                        if (statusInput == "0")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                }
                else if (activeInput == "2")
                {
                    ShowInventory(player);
                }
                else if (activeInput == "3")
                {
                    ShopMenu(player);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        static void ShowInventory(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("인벤토리에 아이템이 없습니다.");
                }
                else
                {
                    for (int i = 0; i < player.Inventory.Count; i++)
                    {
                        Console.WriteLine($"- {player.Inventory[i]}");
                    }
                }

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string inventoryInput = Console.ReadLine();

                if (inventoryInput == "1")
                {
                    ManageInventory(player);
                }
                else if (inventoryInput == "0")
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                }
            }
        }

        static void ManageInventory(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                if (player.Inventory.Count == 0)
                {
                    Console.WriteLine("인벤토리에 아이템이 없습니다.");
                    Console.ReadKey();
                    break;
                }

                for (int i = 0; i < player.Inventory.Count; i++)
                {
                    Console.WriteLine($" -{i + 1}. {player.Inventory[i]}");
                }

                Console.WriteLine("\n0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string equipInput = Console.ReadLine();

                if (equipInput == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (int.TryParse(equipInput, out int equipChoice) && equipChoice > 0 && equipChoice <= player.Inventory.Count)
                {
                    var item = player.Inventory[equipChoice - 1];
                    item.Equipped = !item.Equipped;
                    player.UpdateStats();

                    Console.WriteLine(item.Equipped
                        ? $"{item.Name}을(를) 장착했습니다."
                        : $"{item.Name}을(를) 해제했습니다.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ReadKey();
                }
            }
        }

        static void ShopMenu(Player player)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드] ");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {shopItems[i].Display()}");
                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.WriteLine(">>");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\n구매할 아이템 번호를 입력해주세요.");
                        if (int.TryParse(Console.ReadLine(), out int choice))
                        {
                            int index = choice - 1;
                            if (index >= 0 && index < shopItems.Count)
                            {
                                var item = shopItems[index];
                                if (item.Purchased)
                                {
                                    Console.WriteLine("이미 구매한 아이템입니다.");
                                }
                                else if (player.Gold >= item.Price)
                                {
                                    player.Gold -= item.Price;
                                    item.Purchased = true;
                                    player.Inventory.Add(new InventoryItem(item.Name, item.Stat, item.Description));
                                    Console.WriteLine("구매를 완료했습니다.");
                                }
                                else
                                {
                                    Console.WriteLine("Gold가 부족합니다.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다.");
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "0":
                        Console.Clear();
                        running = false;
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
