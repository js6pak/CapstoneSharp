namespace CapstoneSharp.Arm;

public enum CapstoneArmInstructionId : uint
{
    Invalid = 0,
    ADC,
    ADD,
    ADR,
    AESD,
    AESE,
    AESIMC,
    AESMC,
    AND,
    BFC,
    BFI,
    BIC,
    BKPT,
    BL,
    BLX,
    BX,
    BXJ,
    B,
    CDP,
    CDP2,
    CLREX,
    CLZ,
    CMN,
    CMP,
    CPS,
    CRC32B,
    CRC32CB,
    CRC32CH,
    CRC32CW,
    CRC32H,
    CRC32W,
    DBG,
    DMB,
    DSB,
    EOR,
    ERET,
    VMOV,
    FLDMDBX,
    FLDMIAX,
    VMRS,
    FSTMDBX,
    FSTMIAX,
    HINT,
    HLT,
    HVC,
    ISB,
    LDA,
    LDAB,
    LDAEX,
    LDAEXB,
    LDAEXD,
    LDAEXH,
    LDAH,
    LDC2L,
    LDC2,
    LDCL,
    LDC,
    LDMDA,
    LDMDB,
    LDM,
    LDMIB,
    LDRBT,
    LDRB,
    LDRD,
    LDREX,
    LDREXB,
    LDREXD,
    LDREXH,
    LDRH,
    LDRHT,
    LDRSB,
    LDRSBT,
    LDRSH,
    LDRSHT,
    LDRT,
    LDR,
    MCR,
    MCR2,
    MCRR,
    MCRR2,
    MLA,
    MLS,
    MOV,
    MOVT,
    MOVW,
    MRC,
    MRC2,
    MRRC,
    MRRC2,
    MRS,
    MSR,
    MUL,
    MVN,
    ORR,
    PKHBT,
    PKHTB,
    PLDW,
    PLD,
    PLI,
    QADD,
    QADD16,
    QADD8,
    QASX,
    QDADD,
    QDSUB,
    QSAX,
    QSUB,
    QSUB16,
    QSUB8,
    RBIT,
    REV,
    REV16,
    REVSH,
    RFEDA,
    RFEDB,
    RFEIA,
    RFEIB,
    RSB,
    RSC,
    SADD16,
    SADD8,
    SASX,
    SBC,
    SBFX,
    SDIV,
    SEL,
    SETEND,
    SHA1C,
    SHA1H,
    SHA1M,
    SHA1P,
    SHA1SU0,
    SHA1SU1,
    SHA256H,
    SHA256H2,
    SHA256SU0,
    SHA256SU1,
    SHADD16,
    SHADD8,
    SHASX,
    SHSAX,
    SHSUB16,
    SHSUB8,
    SMC,
    SMLABB,
    SMLABT,
    SMLAD,
    SMLADX,
    SMLAL,
    SMLALBB,
    SMLALBT,
    SMLALD,
    SMLALDX,
    SMLALTB,
    SMLALTT,
    SMLATB,
    SMLATT,
    SMLAWB,
    SMLAWT,
    SMLSD,
    SMLSDX,
    SMLSLD,
    SMLSLDX,
    SMMLA,
    SMMLAR,
    SMMLS,
    SMMLSR,
    SMMUL,
    SMMULR,
    SMUAD,
    SMUADX,
    SMULBB,
    SMULBT,
    SMULL,
    SMULTB,
    SMULTT,
    SMULWB,
    SMULWT,
    SMUSD,
    SMUSDX,
    SRSDA,
    SRSDB,
    SRSIA,
    SRSIB,
    SSAT,
    SSAT16,
    SSAX,
    SSUB16,
    SSUB8,
    STC2L,
    STC2,
    STCL,
    STC,
    STL,
    STLB,
    STLEX,
    STLEXB,
    STLEXD,
    STLEXH,
    STLH,
    STMDA,
    STMDB,
    STM,
    STMIB,
    STRBT,
    STRB,
    STRD,
    STREX,
    STREXB,
    STREXD,
    STREXH,
    STRH,
    STRHT,
    STRT,
    STR,
    SUB,
    SVC,
    SWP,
    SWPB,
    SXTAB,
    SXTAB16,
    SXTAH,
    SXTB,
    SXTB16,
    SXTH,
    TEQ,
    TRAP,
    TST,
    UADD16,
    UADD8,
    UASX,
    UBFX,
    UDF,
    UDIV,
    UHADD16,
    UHADD8,
    UHASX,
    UHSAX,
    UHSUB16,
    UHSUB8,
    UMAAL,
    UMLAL,
    UMULL,
    UQADD16,
    UQADD8,
    UQASX,
    UQSAX,
    UQSUB16,
    UQSUB8,
    USAD8,
    USADA8,
    USAT,
    USAT16,
    USAX,
    USUB16,
    USUB8,
    UXTAB,
    UXTAB16,
    UXTAH,
    UXTB,
    UXTB16,
    UXTH,
    VABAL,
    VABA,
    VABDL,
    VABD,
    VABS,
    VACGE,
    VACGT,
    VADD,
    VADDHN,
    VADDL,
    VADDW,
    VAND,
    VBIC,
    VBIF,
    VBIT,
    VBSL,
    VCEQ,
    VCGE,
    VCGT,
    VCLE,
    VCLS,
    VCLT,
    VCLZ,
    VCMP,
    VCMPE,
    VCNT,
    VCVTA,
    VCVTB,
    VCVT,
    VCVTM,
    VCVTN,
    VCVTP,
    VCVTT,
    VDIV,
    VDUP,
    VEOR,
    VEXT,
    VFMA,
    VFMS,
    VFNMA,
    VFNMS,
    VHADD,
    VHSUB,
    VLD1,
    VLD2,
    VLD3,
    VLD4,
    VLDMDB,
    VLDMIA,
    VLDR,
    VMAXNM,
    VMAX,
    VMINNM,
    VMIN,
    VMLA,
    VMLAL,
    VMLS,
    VMLSL,
    VMOVL,
    VMOVN,
    VMSR,
    VMUL,
    VMULL,
    VMVN,
    VNEG,
    VNMLA,
    VNMLS,
    VNMUL,
    VORN,
    VORR,
    VPADAL,
    VPADDL,
    VPADD,
    VPMAX,
    VPMIN,
    VQABS,
    VQADD,
    VQDMLAL,
    VQDMLSL,
    VQDMULH,
    VQDMULL,
    VQMOVUN,
    VQMOVN,
    VQNEG,
    VQRDMULH,
    VQRSHL,
    VQRSHRN,
    VQRSHRUN,
    VQSHL,
    VQSHLU,
    VQSHRN,
    VQSHRUN,
    VQSUB,
    VRADDHN,
    VRECPE,
    VRECPS,
    VREV16,
    VREV32,
    VREV64,
    VRHADD,
    VRINTA,
    VRINTM,
    VRINTN,
    VRINTP,
    VRINTR,
    VRINTX,
    VRINTZ,
    VRSHL,
    VRSHRN,
    VRSHR,
    VRSQRTE,
    VRSQRTS,
    VRSRA,
    VRSUBHN,
    VSELEQ,
    VSELGE,
    VSELGT,
    VSELVS,
    VSHLL,
    VSHL,
    VSHRN,
    VSHR,
    VSLI,
    VSQRT,
    VSRA,
    VSRI,
    VST1,
    VST2,
    VST3,
    VST4,
    VSTMDB,
    VSTMIA,
    VSTR,
    VSUB,
    VSUBHN,
    VSUBL,
    VSUBW,
    VSWP,
    VTBL,
    VTBX,
    VCVTR,
    VTRN,
    VTST,
    VUZP,
    VZIP,
    ADDW,
    ASR,
    DCPS1,
    DCPS2,
    DCPS3,
    IT,
    LSL,
    LSR,
    ORN,
    ROR,
    RRX,
    SUBW,
    TBB,
    TBH,
    CBNZ,
    CBZ,
    POP,
    PUSH,
    NOP,
    YIELD,
    WFE,
    WFI,
    SEV,
    SEVL,
    VPUSH,
    VPOP,
    ENDING,
}
