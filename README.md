# Virtual-Machine

Virtual machine written in C #. It allows you to perform / emulate the compiled  .net code (il). Execute code can be hibernated and then restarted (even on another machine)

##Example
### simple process / workflow written in C# with hibernation

```c#
public class HibernateWorkflow
    {
        public int InputParametr { get; set; }
        public string Start()
        {
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            //after restore (in another thread/computer)
            //do some work
            for (int i = 0; i < 10; i++)
            {
                SomeInterpretedFunction();
            }

            return "Helow World " + InputParametr;
        }

        [Interpret]
        public void SomeInterpretedFunction()
        {
            //do some work
            InputParametr++;

            //hibernate executed method
            VirtualMachine.VirtualMachine.Hibernate();
        }
}
```

