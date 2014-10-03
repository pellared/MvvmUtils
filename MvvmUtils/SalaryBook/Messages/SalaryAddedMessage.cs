using GalaSoft.MvvmLight.Messaging;

using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.Messages
{
    public class SalaryAddedMessage : GenericMessage<Salary>
    {
        public SalaryAddedMessage(Salary content)
            : base(content)
        {
        }

        public SalaryAddedMessage(object sender, Salary content)
            : base(sender, content)
        {
        }

        public SalaryAddedMessage(object sender, object target, Salary content)
            : base(sender, target, content)
        {
        }
    }
}