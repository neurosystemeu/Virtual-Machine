using Mono.Cecil.Cil;

namespace NeuroSystem.VirtualMachine.Instrukcje
{
    /// <summary>
    /// Converts the boxed representation of a value type to its unboxed form.
    /// https://msdn.microsoft.com/pl-pl/library/system.reflection.emit.opcodes.unbox(v=vs.110).aspx
    /// </summary>
    internal class Unbox : InstructionBase
    {
        public Unbox(Instruction ins) : base(ins)
        {
        }

        public override void Wykonaj()
        {
            //nic nie robię - box i unbox jest robiony przez środowisko wykonujące
            //nie muszę tego emulować
            base.Wykonaj();
        }
    }
}