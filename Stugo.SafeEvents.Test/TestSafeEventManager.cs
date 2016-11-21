using System;
using Xunit;

namespace Stugo.SafeEvents.Test
{
    public class TestSafeEventManager
    {
        [Fact]
        public void It_calls_a_registered_handler()
        {
            TestObject.InvokeCount = 0;
            var manager = new SafeEventManager<object>();
            var message = new object();

            var target = new TestObject();
            manager.AddHandler(target, x => x.TestHandler);

            manager.Invoke(message);

            Assert.Equal(1, TestObject.InvokeCount);
            Assert.True(object.ReferenceEquals(message, target.Message));
        }


        [Fact]
        public void It_doesnt_call_an_unregistered_handler()
        {
            TestObject.InvokeCount = 0;
            var manager = new SafeEventManager<object>();
            var message = new object();

            var target1 = new TestObject();
            manager.AddHandler(target1, x => x.TestHandler);

            var target2 = new TestObject();
            manager.AddHandler(target2, x => x.TestHandler);

            manager.RemoveHandler(target2.TestHandler);
            manager.Invoke(message);

            Assert.Equal(1, TestObject.InvokeCount);
            Assert.True(object.ReferenceEquals(message, target1.Message));
        }


        [Fact]
        public void It_can_clear_up_dead_references()
        {
            TestObject.InvokeCount = 0;
            var manager = new SafeEventManager<object>();
            CreateABunchOfRefs(manager);
            var message = new object();

            var target = new TestObject();
            manager.AddHandler(target, x => x.TestHandler);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            manager.Invoke(message);

            Assert.Equal(1, TestObject.InvokeCount);
            Assert.True(object.ReferenceEquals(message, target.Message));
        }


        private void CreateABunchOfRefs(SafeEventManager<object> manager)
        {
            for (var i = 0; i < 100; ++i)
            {
                var instance = new TestObject();
                manager.AddHandler(instance, x => x.TestHandler);
            }
        }



        private class TestObject
        {
            public static int InvokeCount;


            ~TestObject()
            {
                // provide something to block WaitForPendingFinalizers with
            }

            public object Message { get; private set; }


            public void TestHandler(object message)
            {
                ++InvokeCount;
                Message = message;
            }
        }
    }
}
