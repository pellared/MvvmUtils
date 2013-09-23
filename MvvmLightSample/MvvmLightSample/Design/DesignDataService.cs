using MvvmLightSample.Model;
using System;

namespace MvvmLightSample.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data
            var item = new DataItem("MVVM Utils in design");
            callback(item, null);
        }
    }
}