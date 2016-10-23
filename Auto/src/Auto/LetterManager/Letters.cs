using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.LetterManager
{
    public class Letters
    {
        private static IsNear _near;
        public static IsNear GetNear
        {
            get
            {
                if (_near == null)
                {
                    _near = new IsNear();
                }
                return _near;
            }
        }

        private static IsCommon _common;
        public static IsCommon GetCommon
        {
            get
            {
                if (_common == null)
                {
                    _common = new IsCommon();
                }
                return _common;
            }
        }
    }
}
