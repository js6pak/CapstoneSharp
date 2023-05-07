# CapstoneSharp

Strongly-typed fast C# bindings for capstone

- [x] arm64
- [x] arm
- [ ] x86 (you can just use [iced](https://github.com/icedland/iced) instead)

```cs
var disassembler = new CapstoneArm64Disassembler();

var code = new byte[] { 0xFF, 0x43, 0x00, 0xD1, 0xE0, 0x0F, 0x00, 0xB9, 0xE0, 0x0F, 0x40, 0xB9, 0x00, 0x7C, 0x00, 0x1B, 0xFF, 0x43, 0x00, 0x91, 0xC0, 0x03, 0x5F, 0xD6 };

foreach (var instruction in disassembler.Iterate(code, 0x0))
{
    Console.WriteLine($"{instruction.Mnemonic} {instruction.Operands}");
}

// Output:
// sub sp, sp, #0x10
// str w0, [sp, #0xc]
// ldr w0, [sp, #0xc]
// mul w0, w0, w0
// add sp, sp, #0x10
// ret 
```

For more examples look at the [tests](https://github.com/js6pak/CapstoneSharp/blob/master/CapstoneSharp.Tests/Arm64DisassembleTest.cs)
