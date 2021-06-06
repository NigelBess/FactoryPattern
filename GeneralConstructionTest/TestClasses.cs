using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralConstructionTest
{
    interface IImplementor
    {
    }
    class FirstImplementor : IImplementor
    {
    }
    class SecondImplementor : IImplementor
    {
    }
    class DoesNotFollowNameConvention : IImplementor
    {
    }
}
