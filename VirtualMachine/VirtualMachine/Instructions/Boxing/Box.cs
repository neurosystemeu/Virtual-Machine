using Mono.Reflection;

namespace NeuroSystem.VirtualMachine.Instructions.Boxing
{
    /// <summary>
    /// Converts a value type to an object reference (type O).
    /// https://msdn.microsoft.com/pl-pl/library/system.reflection.emit.opcodes.box(v=vs.110).aspx 
    /// </summary>
    internal class Box : InstructionBase
    {
        public Box(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {
            //nic nie robię - box i unbox jest robiony przez środowisko wykonujące
            //nie muszę tego emulować
            WykonajNastepnaInstrukcje();
        }
    }
}