# Virtual-Machine

Virtual machine written in C #. It allows you to perform / emulate the compiled .net code (IL). The code is executed instruction by instruction. (Virtual machine emulates all instructions from IL)
Execute code can be hibernated and then restarted (even on another machine after serialization)

##Example
### simple process / workflow written in C# with hibernation - serialization
An example of a process that will be executed on the virtual-machine.

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

launching
```C#
var proces = new HibernateWorkflowSimple() { InputParametr = 10 };
var vm = new VirtualMachine.VirtualMachine();
vm.Start(proces);
var serializedVMXml = vm.Serialize(); 

//

var vmNew = VirtualMachine.VirtualMachine.Deserialize(serializedVMXml);
var retFromSerializedVM = vmNew.Resume();

//compare the result with the normal execution process
var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
var retInProcProces = inProcProces.Start();
Assert.AreEqual(retInProcProces, retFromSerializedVM);
```


