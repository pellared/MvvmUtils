using System;

namespace MvvmLightSample.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}