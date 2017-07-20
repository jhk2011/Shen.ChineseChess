using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Shen.Net.Common
{
    [Serializable]
    public class Message:ISerializable,IDeserializationCallback
    {
        Dictionary<string, object> _values = new Dictionary<string, object>();

        SerializationInfo _info;

        public Message ()
	    {

	    }

        public Message(Dictionary<string, object> values)
        {
            _values = values;
        }

        private Message(SerializationInfo info, StreamingContext context)
	    {
            _info = info;
	    }

        public Dictionary<string, object> Values
        {

            get { return _values; }
            set { _values = value; }
        }

        public T GetValue<T>(string key) {
            if (_values.ContainsKey(key))
            {
                return (T)_values[key];
            }
            else {
                return default(T);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string,object> pair in _values)
            {
                sb.Append(String.Format("{0}={1}",pair.Key,pair.Value));
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();

        }

        

        #region ISerializable 成员

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("values", _values);
        }

        #endregion

        #region IDeserializationCallback 成员

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            if (_info == null) return;
            _values = (Dictionary<string, object>)_info.GetValue("values", typeof(Dictionary<string, object>));
        }

        #endregion
    }
}
