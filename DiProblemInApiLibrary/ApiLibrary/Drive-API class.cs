using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLibrary
{
    // This is an API class that cannot change (used by other vendors, projects etc.
    // Additionaly we really do not want to expose the interfaces
    public class Drive
    {
        private readonly IMotor _motor;
        private readonly ISpeed _speed;

        public Drive()
        {
            _motor = Bootstrapper.Get<IMotor>();
            _speed = Bootstrapper.Get<ISpeed>();
        }

        public void StartMotor()
        {
            _motor.Start(_speed);
        }

        public void StopMotor()
        {
            _motor.Stop();
        }


        public int MotorSpeed
        {
            get { return _speed.KilometeresPerHour; }
            set { _speed.KilometeresPerHour = value; }
        }
    }

    public interface ISpeed
    {
        int KilometeresPerHour { get; set; }
    }
}
