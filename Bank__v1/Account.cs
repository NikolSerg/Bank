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
        T TopUp(double amount);
    }

    interface ITransfer<in T>
        where T : Account
    {
        void Transfer(T from, double amount);

        Account Get();
    }

    public abstract class Account : ITopUp<Account>, ITransfer<Account>
    {
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

        public Account TopUp(double amount)
        {
            AccAmount += amount;
            return this;
        }

        public void Transfer(Account from, double amount)
        {
            from.AccAmount -= amount;
            this.AccAmount += amount;
        }

        public Account Get()
        {
            return this;
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
