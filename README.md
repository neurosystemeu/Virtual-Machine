# Virtual-Machine
Version during the translation - I'm sorry for the mistakes

Virtual machine written in C #. It allows you to perform / emulate the compiled .net code (IL). The code is executed instruction by instruction. (Virtual machine emulates all instructions from IL)
Execute code can be hibernated and then restarted (even on another machine after serialization)

##Example
### simple process / workflow written in C# with hibernation - serialization
An example of a process that will be executed on the virtual-machine.

```c#
public class HibernateWorkflowSimple
{
    public int InputParametr { get; set; }
    public string Start()
    {
        //do some work
        for (int i = 0; i < 10; i++)
        {
            SomeInterpretedFunction();
        }

        //hibernate executed method
        VirtualMachine.VirtualMachine.Hibernate();

        //after resume
        //do some work
        for (int i = 0; i < 10; i++)
        {
            SomeInterpretedFunction();
        }

        return "Helow World " + InputParametr;
    }

    public void SomeInterpretedFunction()
    {
        //do some work
        InputParametr++;
    }
}
```
[full code](https://github.com/neurosystemeu/Virtual-Machine/blob/master/VirtualMachine/UnitTestVirtualMachine/Example/HibernateWorkflowSimple.cs)

launching
```C#
var proces = new HibernateWorkflowSimple() { InputParametr = 10 };
var vm = new VirtualMachine.VirtualMachine();
vm.Start(proces); //execution to the end or to hibernate. At production should check the status of a virtual machine
var serializedVMXml = vm.Serialize(); 

/////////////////////
// we can deserialize and execute on a different computer
var vmNew = VirtualMachine.VirtualMachine.Deserialize(serializedVMXml);
var retFromSerializedVM = vmNew.Resume();

//compare the result with the normal execution process
var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
var retInProcProces = inProcProces.Start();
Assert.AreEqual(retInProcProces, retFromSerializedVM);
```
[Full test](https://github.com/neurosystemeu/Virtual-Machine/blob/master/VirtualMachine/UnitTestVirtualMachine/Example/UnitTestExample.cs)

### the process with interpreted functions (subfunctions)
```C#
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
[full code](https://github.com/neurosystemeu/Virtual-Machine/blob/master/VirtualMachine/UnitTestVirtualMachine/Example/HibernateWorkflow.cs)

launching
```C#
 var proces = new HibernateWorkflowSimple() { InputParametr = 10 };
var vm = new VirtualMachine.VirtualMachine();
vm.Start(proces);
var serializedVMXml = vm.Serialize();
object retFromSerializedVM = "";

while (vm.Status == VirtualMachineState.Hibernated)
{
    vm = VirtualMachine.VirtualMachine.Deserialize(serializedVMXml);
    retFromSerializedVM = vm.Resume();
    serializedVMXml = vm.Serialize();
}

var inProcProces = new HibernateWorkflowSimple() { InputParametr = 10 };
var retInProcProces = inProcProces.Start();
Assert.AreEqual(retInProcProces, retFromSerializedVM);
```

## Dependencies
[Mono.Cecil](https://github.com/jbevain/cecil) - will be removed
[Mono.Reflection](https://github.com/jbevain/mono.reflection)
[sharpSerializer](http://sharpserializer.com/en/index.html)

## Used in
[Native-Workflow](https://github.com/neurosystemeu/Native-Workflow) - workflow engine for long run process/workflow. Allow to serialize executed workflow and resume. It has a fluent GUI through a process can communicate with the user. (WWW and Console GUI)

