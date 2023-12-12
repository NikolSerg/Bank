using System;

namespace Bank__v1
{
    public static class AccountManager<T>
        where T : Account, new()
    {
        public static T OpenAcc()
        {
            T acc = new T();
            return acc;
        }
    }

    interface ITopUp<out T>
    {
        T Get();
        T TopUp(double amount, User user);
    }

    interface ITransfer<in T>
        where T : Account
    {
        void Transfer(T from, double amount, User user);

        Account Get();
    }

    public abstract class Account : ITopUp<Account>, ITransfer<Account>
    {
        public event Action<User, double, ulong, ulong> OnTransfer;
        public event Action<User, double, ulong> OnTopUp;
        public event Action<User, ulong> OnClosing;
        public string AccType { get; set; }
        public ulong AccNumber { get; set; }
        public double AccAmount { get; set; }
        public bool IsActive { get; set; }

        public virtual DateTime OpenDate { get; private set; }

        public Account()
        {
            AccAmount = 0;
            IsActive = true;
            OpenDate = DateTime.Now;
        }

        public Account(ulong accNumber, double accAmount, bool isActive)
        {
            AccNumber = accNumber;
            AccAmount = accAmount;
            IsActive = isActive;
            OpenDate = DateTime.Now;
        }

        public Account TopUp(double amount, User user)
        {
            AccAmount += amount;
            this.OnTopUp?.Invoke(user, amount, AccNumber);
            return this;
        }

        public void CloseAccount(User user)
        {
            this.IsActive = false;
            this.OnClosing?.Invoke(user, this.AccNumber);
            switch (this.AccType)
            {
                case "Депозитный":
                    this.GetOwner().Accounts[1] = null;
                    break;
                case "Недепозитный":
                    this.GetOwner().Accounts[0] = null;
                    break;
            }

        }

        public void Transfer(Account from, double amount, User user)
        {
            from.AccAmount -= amount;
            this.AccAmount += amount;
            this.OnTransfer?.Invoke(user, amount, from.AccNumber, this.AccNumber);
            if (this.GetOwner() != from.GetOwner())
                from.OnTransfer?.Invoke(user, amount, from.AccNumber, this.AccNumber);
        }

        public Account Get()
        {
            return this;
        }

        public Person GetOwner()
        {
            Person owner = null;
            foreach(Person person in Person.Clients)
            {
                foreach(NotDepAccount account in person.Accounts)
                {
                    if (account != null && account.AccNumber == this.AccNumber)
                    {
                        owner = person;
                        break;
                    }
                }
                if (owner != null) break;
            }

            return owner;
        }
    }

    public class NotDepAccount : Account
    {
        public NotDepAccount() : base()
        {
            AccType = "Недепозитный";
        }
        public NotDepAccount(ulong accNumber, double accAmount, bool isActive) : base(accNumber, accAmount, isActive)
        {
            AccType = "Недепозитный";
        }
    }

    public class DepAccount : NotDepAccount
    {
        public DepAccount() : base()
        {
            AccType = "Депозитный";
        }
        public DepAccount(ulong accNumber, double accAmount, bool isActive) : base(accNumber, accAmount, isActive)
        {
            AccType = "Депозитный";
        }
    }

}
