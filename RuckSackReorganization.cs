using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class RuckSackReorganization
    {
        private class RuckSackContents
        {
            public RuckSackContents(string items) 
            {
                //22, middle = 11. 21 items, 0-10, 11-21
                int middle = items.Length / 2;
                Compartment1 = items.Substring(0, middle).ToList();
                Compartment2 = items.Substring(middle, middle).ToList();
            }

            public List<char> Compartment1;
            public List<char> Compartment2;
        }

        public RuckSackReorganization()
        {
            Console.WriteLine("\r\n\r\nRuck Sack Adventure, GO!");
            Console.WriteLine("----------------------------------");
            var rucksacks = RuckSacks.Replace("\r", null).Split('\n');
            foreach(string s in rucksacks)
            {
                RuckSackList.Add(s, new RuckSackContents(s));
            }
        }

        public void FindBadgeSum()
        {
            int sum = 0;
            string[] elfGroup = new string[3];

            int count = 0;
            foreach (string contents in RuckSackList.Keys)
            {
                elfGroup.SetValue(contents, count);
                count++;
                if(count == 3)
                {
                    var group1 = elfGroup[0];
                    foreach(char a in elfGroup[0])
                    {
                        if(elfGroup[1].Contains(a) &&
                            elfGroup[2].Contains(a))
                        {
                            sum += GetItemPriority(a);
                            break;
                        }
                    }

                    count = 0;
                }
            }

            Console.WriteLine("Total Badge item priority is: {0}", sum);
        }

        public void FindPrioritiesSum()
        {
            int sum = 0;
            foreach(RuckSackContents contents in RuckSackList.Values)
            {
                var dupe = FindDuplicateItem(contents);
                if(dupe != null)
                {
                    sum += GetItemPriority(dupe.Value);
                }
            }

            Console.WriteLine("Total dupe item priority is: {0}", sum);
        }

        private readonly Dictionary<string, RuckSackContents> RuckSackList = new();

        private char? FindDuplicateItem(RuckSackContents ruckSackContents)
        {
            foreach(char a in ruckSackContents.Compartment1)
            {
                if (ruckSackContents.Compartment2.Contains(a))
                {
                    return a;
                }
            }

            return null;
        }

        private int GetItemPriority(char item)
        {
            if(ItemPriorities.ContainsKey(item))
            {
                return ItemPriorities[item];
            }
            else
            {
                //then it's uppercase
                return ItemPriorities[char.ToLower(item)] + 26;
            }
        }

        private readonly Dictionary<char, int> ItemPriorities = new Dictionary<char, int>()
        {
            { 'a', 1 },
            { 'b', 2 },
            { 'c', 3 },
            { 'd', 4 },
            { 'e', 5 },
            { 'f', 6 },
            { 'g', 7 },
            { 'h', 8 },
            { 'i', 9 },
            { 'j', 10 },
            { 'k', 11 },
            { 'l', 12 },
            { 'm', 13 },
            { 'n', 14 },
            { 'o', 15 },
            { 'p', 16 },
            { 'q', 17 },
            { 'r', 18 },
            { 's', 19 },
            { 't', 20 },
            { 'u', 21 },
            { 'v', 22 },
            { 'w', 23 },
            { 'x', 24 },
            { 'y', 25 },
            { 'z', 26 }
        };

        private static readonly string RuckSacks = @"DPstqDdrsqdDtqrFDJDDrmtsJHflSJCLgCphgHHgRHJCRRff
BcBGcQzVBVZcvznTTTvZcGTpCRRRfRCggLflHlhhCZpZCj
vGQnQvnzTzNTTbVnzGBqMqwqDLdPtMmbwqqLLM
wLRFRqvFsFRjfrHddbdbjzdH
lcsnSJPSSVVlGmGrHzbbrGNrdzbz
mSmlnnPlmJmncVDSlSZSlmLBCvtwBvtLCqqswsDBCTWW
pfqPrPgmmhvqdlsdWq
nfjHLJfZcLbVtQWWtndhls
CzJJFLzRzfDwrmggpC
CWfllmlCDFlZZqMfmFBWmWLJVRLVwNNtRVGPpwtGpqbJ
jHndndndcjhscnhHNtRbVtLbGpJbRRcb
HSrvnQzQSMDlLzBCfg
BQRVbgQQBJBbBtVBSSSRWMQbdNvvRPjZjCCdPLNZNNsNCCzd
HwpFpnlGpGZWGvjzPd
FTDmFrrwDpFMtmQVQQcWgc
VhbPshVDPDFWhWsgDNMMbVtmBjwBffpwBntnmnqfnswt
QzzGrTZZdrdlTcCLpRBnmBRRjBCqtptt
rJdGlmLTJdJrvZDbSbDSWDNbFJgD
qrcqTBHTcHgwWWdHRjdWBglBbGPpGvvPbszGzsbPpQfPLwPz
nFVmjhMjFJCSJsQQPQLbLpzCPQ
SnMSVVZSJMNMZNDVFtJtNRBdqBrWrRHWZTllrrjgHq
ZqdqcrPqqrwnQqnrZqjVcqrQwwmNbzNzwbNLzvFbHLbNmBLF
LCDsCsRTfLTDszzNbsbNNHbs
gLfCgShfCgMPlPrVcqrQgn
QSNSLDQDLfqqPwwBNLqgqJMMmmRRCTzHnCHhRzHmfCmh
lGvdbdWdVvsVszpDhHmmlnMpTC
ctbdtVsbbvvsbWZFdVQJqPtgLQBDBwBQPJwJ
dggSSDCPddRWPnSSPWRDgdSrTDsDQDTzQGGTMbsMMVsQfTfV
jmBvtFpBcBhhjljZHphztMsCbsTTCbzsqGqsfz
hccpLmhFlcwCrPLrCPnL
MMHZnGrCfJnfCPggSSGGSSSgLW
qhFhRlDFDlqFsgdvJdWdDdcSvp
wVlhqTbbRNFqswlVVRNbZfHCrntMBrTTZnJMCfnn
sHGZscVGHJMtmRrqzRqqqTqt
SjvvNgjLShWWhhSQNWqmrBzlRllTTgBqnRmq
LNQWLfWhSQvLdCddWWPHMcbHHrJcDGFZCJssFM
mSDjSVQbVGbmqDVbHmqqJTZzPHTHhhRJhwsRPcRJ
tFfFFttFdsNntfpMMppJWwZTzJczJcZPzwWcdJ
vNtgCrpgNptptgCCbbmjbSbvsVjSsjGG
VCQlZJCTPRWsBsjTTT
wvNrnbbvnhdNhLMfGsrGpRFpGpjp
dwndHbHbbLqwwhNcLsqSHSCHJClQtJSttQDPSJ
ZlrvrdvpGBBhlDrshdqJHRPHqPTJzRPPqw
tcftfSgFFgcgLPmPmpnqFwJRHP
pWLCcWNNNNttMNgZvlsvrBrsrjWDjB
wgdCJgDMDwMCwDMCMJsJJfpffVpVfbfrrrrgjgllZp
QFRhvttRthtQzzmpBWbWzWZSVpbSpl
btRttRLttGNqvbFHLwCdDcMwnPPJDnDD
VhmMNllLqGLJQNhRfZHgSPfgSPTqZj
sBwDcwBtsdzvvHZRlPRjDZTgPZ
pWvvBBcBCdzNLVQVWQlNlW
NsSppvSjSPNBNLJJLh
fCGtqQbZZGZQZTbtzbqbCZThddcMBddlJGhdlBMcddgLlJ
zZFTqwLtTRFqTQwvmprnRVSsDrnvVR
FttFTzzvlVHFzTjpbvzbFSDDdVGhdqLGWGJdVDDfsLqG
cmBNCRnwsCcBPMfLLfJGcWhWqfdh
BwPmZZMmZMCsnrwMrmbHHbjbSvSbjvrlHpzj
sZQHCBFHQQQPGQCCHCHwsHFshhtSnnqjbRSSPngnhbRjqVPn
mzLvmDvNNWvNvrzzrMTzJNjqndqbnSnnRgTdtjdbjhTt
WzzWDlJLzLDvMWJJlMzmLJWcpHFQBpBgBHGQGBHfwwBfQQlG
gdpFrdrmrDsqqswdtccgWWCMlChSbhqSlCzBlSqh
TTvjrfjNJPnRQNTQjvNnCSWBVBVbClPSVSbSVhSh
HRvnfTfvjjHZTDsmcHDsrDsdmp
bFChjhbpbjqsntjtns
vdWcfMHfddvrlNMNdWWTNgBqDngBBZBBQZshgSfgnD
JlwrlrlhlcJWcWMwhWNVFpLzwPbbLRFPppVLzm
DtBtgLvgcHzllsTwzSTg
vhhjZrCrZdVdZVSwPMwwTTMGwT
nmpfqrnZJbqBBvRc
nMvSLvWSWPVPvWnSLShFLBjVbpNVGGbVQbbNcBcBBc
sTzJsJszbbQbdQJb
DsDrwTtsCTFhLQSShRwh
RNFQhTQqHNNGRsNqQFNsHhFCwwPLwPqwzfPrrPBwpJSJJw
vMMMblZjddlvWbjbBBfbwCrPPLJppwCL
jDmvcDBlBdjVglgddmvDQRNFtFRGtQhstHNsGFHV
rhLHmZnMrRsZSstZLLtZnhSCNbbmPJVcblTNNTlccpNTjJTj
WFgGddGFFgFDddMblpJjlTJTPc
QGWqBBfWgBqWwFwzMGvzDqSSrHnCHsrssCRZrfRhHLfH
HHzcWqNPmZcqFHPZGBdMRBMDlllWpRDJMl
tTgSvPhbMDJlbJQb
SCTtvtSPftswjvPhTgffVqmGZLmqmCCcqZzZHFNznF
QNpppRrdZvdgzpQZNpgRRgbSwmDDvFGGqwJSsvSGSqGG
HchWBMcBVnnWcHPjHhWcjHTqJFDGMSSqDMwJJbGwSsGGSb
tcCVcBjPjhnWlFrCFNZflQNr
HsVMrqrPqvvgprSrLG
THJWBJDwRFvBgGSzgF
DmhfHnmQncMNVMqPqbcd
SqZmMJqvHJBhHJLp
wsgTVTSsPssjjFVrTrFlhLhCFlBBnHplHLLHfF
zgggdwPrRrsrjjgRwVwdwdQTmvMvZqDZbWqqMSNWNbGQbGZN
fBDBfLZnTLZVVmmDcQMDDV
jPFtJFpHpJqfJFrptwrJdRWRWNpQVmQRMWNVVVNVvQ
zHwJgtFrTlslfghf
wMwTttCCTTSTfBmPzPVZnPZLVVtbnN
ldRRRlRHggGcvcRbZsNzvBVWnnPBWv
hdlJHgpcJccJhQdJrcrhwFMpDqCwCMBqjSqjqpTC
fJfnwJJnnHJgJHTgjsjDccNjcbgNjm
VdLqRRqGVqpRrPpppMBjDNmDctdsBlNjmZdZ
PDQvPQSvpGDrTwfJzzfFnTnS
MnHvnHHMRMzPTlDLPPRGcl
dFnfhFVwhdBPBfGWlPcP
JNrQFsnVtwsgvNzvmMjpzS
BZVPFpNpcNZpmRRPpzcVNhLLnssDjjDGnqjjLDFDjq
mMJbJvtJQQHlJDGCDnjvChDSsv
MQwWJHdQwWrJltQrgfNPmfBcBrpBpZ
ZWZqDsZZqWsWvWLPwPbpHjdtSbSjSCSPPSCp
MFVNMLmFmNzcTTrFrLbjdjbpCdCSbTCShRSd
czNzLrznlGNNrMzMwDlJwJWDwJwqJDvW
GlgchGGVShlQcQfDhzZrNFnFNFNjFzNFcn
dwCtpwHTtPTWpdFNfJJzRzvJNR
tLBBmWHftBttPbLwCHWTsSSQVglqgMsMBDSMGlQS
RDDDGhGfvPPTTPTThn
ZFLMmjpCpfMZzFqmqsCmPjdVBlVBVnWBPNTVbnTV
zHqJMCzLvftRQQHG
nTcbnvPsvdvFzpczVZmMGg
BCCJwSDqhQLJmMMpzGZVFVFB
qhrwJwrJrrzJNqwWLsTTnlTlbnsvbstWsW
vHRbqPJZvRPZhShJvTZllZtgzwlfBGBlsm
VdQjVVCssQVrWrQmTBgBzglmgCBGml
NnpQNpcFpNWshPRLsbSFsH
cVGmVZVwVVMLdvcRttTdbB
ppCQrwzHBtLrttLb
hsFJQzFWCpCqjZGVwlhlPP
HDGRzgWhgfzVWfRpspwRwbwStSwt
ZBPPPmmmTMQMPcZrBmSptSbbQCwtlsNqCwjC
TTLMMmZvPTrMZvFMmcmvrTccDDnfGHJgJhHhnhnLfVhSWDJz
pNrpjzthZPnGrzzWbJLLLbbJZwgSvZCV
MQsFFFDTfMNfRFfBFMdBdwLSvgbSTVCqTgVbbTLvwV
BQlQDMFccQsNmWpPGhpcjr
CTgGRCRglLlLTllL
vMJmhPJcmPBMvhqPDnNNqlWnwDWqsRQs
hcBfcJRPFfvvRvJZBrfMPZdpbSSGtSdtdgtzzSZzbV
NQLzNzzJcrLrSgZSSGgZrR
bTsjqHvcmTHvjgZGDvDpGZRfpg
WqTVPbdnMlLJncQC
hZLBrqLGLMbzLLBhfMMrnnNJlnNnlnJJNNdCJdzN
TWTsWqvtvpTSgRHpVFdjgjCPdgJlCFCFnF
swSTsTwpTVRmVRRRqMDMfqfDwLfbrhLr
NTQHWNQWrQwSTDWlcPPBHZBZbPgZJZ
nmfjCRCfRhndJcjBbcbcbg
nsppRfssfzCnqgzTzrwTwQVTWM
mFjQmDGmbbGjmChrCwdQBHCHWh
qvZZnPvvnngMpnlqpMZnpsTgWHTRCWrVdVDBWRhHBrhHDHhd
vqZgnnqgLvqlPllpnjGDjmNjNLfftftLFD
rfGsjsMNnFMMFddMsttDMgLHGlmJLCPPmHHGmHPlmm
vZcbhQbrRbVZPJLwPTgCLlgb
hchzSBqzQvphnWnrjFdWMqff
WmfPWfVsfqszRDqPqgpvHhvdwddGMmGghM
QtTrtTcSBjtQCctStrTrzhpwjGvGHhngwMHGHvMv
TtTQlQFcSSJlcccBbltQQTTRsPZDsWzRFzWFzPNfsssLPs
QpNNMrjcNMccGNdvLBBlBsBjnsnF
tTSqbbbqCtWWCTWSVTmmCJPwVwnwvFPnsPVnnPfddlvf
HmhJTZWqHqCJJJltqpNGRgzZDcQNrgzDGM
HcLVRhhTRsLRRVjslTscqNQmVNQQgQttqNwNZtmw
nJdBJJhfFPSCbJBJBMbzFbFgmNmtgmvgNgnntNwZQQNNmw
bMbPzJbzCBPrJfdfbBbdCrGHlLTTpWjsGhGTTRTRlc
sJCCpQJQCrfCfnSCrT
vmqgNggzgmZqmPShqBhThfhDhjDhhB
RZNzHRzZSQwHwHVVWc
jtVtvVHgvjJbHjjQPMZdCcwlMdNbdFlNlc
WppSBDzGfBzTBqQWwCFMlwZMwMcZ
zBfnqpRGnSSqTfqpTpSnnHQsjJgQvPJshHtHVh
qJMRMcPPVzVhmsDWfhWT
BglQBNlgZtQBHLHHBnTjWSWmFmwDmWjSsnmF
BdHvgHBvBtZbTpJRPCdcdpGrGJ
pcGcWGWlvQZpzmDbgFmz
HqqnddDdddjzTTggjZgFtT
sHqRwrRsJswLHrMLLRJdqNVVrGffPGWcvSSWlDfGfc
lttTbgRvqvtQRhjLzGjLVh
JJfrHfrdffZJQmZhLLZVVwFj
sBjCfSNNTTqnCnqD
qMtWjSrHftGfjqrJGMqzVzFmBBrzQQwzgBVQVQ
LDChPbThbTcTpCTcnPPQPQzVPvvzQBBWgVBQ
ZLspppLpdZZttdHttqdWMf
htJcJhpMQQWjhNWdJQSCFCTvFBPCTDlMmDCFlM
jjbbsfjwZbLGVVqHCFPvmvDmClTfmP
zjVVRwZwnRJtnNQt
PCPVSzLMMRqGwgMmHmQmDQ
slrrbZZgsfcdsgdhrHFGQHQFwvfwFwDGTv
NclhgpctrrNjllcZdcrpZnPPqzLLSSLqJLtJWCWzCn
PBLSBPVBwpTVppfT
lZCqQQtCQGPJJPtPHHwTwZTTZpwHsfRH
mCtGFDqFGDGQjPGqjJMMlqPgdWgSSgBWWcWzLdgvMzgBMg
cLBrfchhFBcnrgvqvPGvvwSS
QpzpstDDZMwDZqwh
WzpbWTjsbhpQtjThsjJFRNLnfLbfRLRBLlFB
ngnWWqnfgqtfsrWftqsrFWPSdSSdRCTHRSwpRGTfGmSG
VhJhVczJQcvbvvlhBpvlPdmlwHRTGHSRPTSCRGTd
zVcBcMhzcVcvMJJJDpWrsqrtrWLWsgZZFtFD
fbccrJlrffTwJDJTtBtB
hRNNFddsgsFPLLRVVwthMCQTtBwrtT
jrsPGLNjsqPlvGbZbcvScz
HFPmmgQrQzFgrLVPPrLFPNDJNJzzcGbJTbsSzbGGNc
MtvCMhJBdnMhwfhlwnfBfMDCDSjGbqDNGNGqGjDDjsDC
wwnhhdtBBptwdlhlRntRldJFVWRFPmWZFmHRZZmFQPRWmm
WrHNNTBNTTTBwHHcSTrBnSzJPFnpJfpLVfpDVdJLFJLFdD
hRthQvhRQlQmDpfVJFdLlLLj
hMZZbCMvgQgBTBGNGDcWbr
HvQjMRMTzjsCQzHTCFfVVZLPVvfLfPVpZg
GtlbBtSGlSbDdStrhSFCPVDgZgLLgPpPJWPF
rmwSbcbcdbrbGljQjCzCCwnHRqQT
bbgNSHPPgnmMMZtNcMpp
VFzFDFVtCBFDCVFdMlhZMhdhmhmplwZL
JVtBjGRFRttFCGDFGJJDQQgSgTWPPfSSfWbQnHvPWW
NvdBpwNvGNFvpBGGBmLFblrtVTwDttlhtlblfQbQ
SCMMsWCMSRZCqsmWcRWgRRsVtlrtrQbtQftThrQTQtqrtQ
WRscMgZJJCJWzZgSWNLFdBNHGzpFGmBFFG
qghqRVzhLNRLqzLhVztgQdLFdrccCnSpcZdSZcTS
DwvmHDJDsmvDGmHbQBlslMDDCrTCnppTndrdBrFnFTSdCCnZ
GGwJHlGwwvMHJljwwDMVtfhtWWhzqVPPjfQRqz
BsDMPrqPzsDwwCLGmqjpjm
VfFJQlVQcvfwJLJCJppLNp
vfcSHCglCgbgbbbFvSlvQfPsrsZrPzZzDWWStPhtZPDP
gjMsnFgbnllbjMfSZBHHtpHvvvFwhv
DDRZDLdVCLNLJwBCShQHHwwBzv
DNJNTLJRTqWmjWZnjrlmjW
ZTSVSFZCLTnvzfzqvnNL
PfPcJljfMpvtlnztvQtw
PsJMMMWpGcgMHMfjRBThrgrTbBSBFdVSTF
GccBRWjgtQqsTcVQcw
JhJCMJHPLffMChlfLCLHMMrDQsQqDVQsqTDbVvGqDhhzqD
dHGlfnCHlJrNmtdptggpmW
wnDDSBCSBSDLzLLmHLrlwlmpTTqzGJJfpjfjNpfqbpbdpG
MMRhFWWRvZPZRZQhFZMVhVSNqjqpNQffJjbjfbjdTJbp
VMMsWcZRWgMPvRSrSHmsrrtwSHnr
TQchPTgjBcNgPHhhThtNzQdzdsCmRDJnzCCmCdCm
vllVwrfvbSBVFSbVGwlrFGlqRCDzRJCJdzvJRzsdLDDLsdCm
VrMrqSWbfqWbBhhpWjNTttpjjP
rsfvSHHcvwrMPtcQZgnDhGdvJzngLzzJLJ
lWmVlfbCCNFCpBCmTpFFJgzhDLGhmRGhLhmGdgGR
pNBfflVTNpfWTWbWWbjNVqBsscqsqrZSwMwZrMPZSZrZrs
PJPHPJmhhHhlHPQgCndngTbWnqCWDGTD
tSwccFpFqwMcFbGFWvnnWvCW
MwLwLMSwpNBBtctSctfhZHJQhhqmlRlZRNPH
GNzdZhVGvtGZVVgGgtfHHWhpLPPpLWpWWnHf
RjwqRcDTvCrWJWWnlLnnqn
DbrDDwwBwjjsrbDTRTBmwgZmgGgmdttvQvQFSQGFtg
jRgcZRfhmHfZjPZRgHffLFTzzddBTBBFzLDZzBTF
VtsJwSbcStlwMqbtwbvWBWddGGdrFDDWJWrzTT
VwsQQvlbVbVlbNllVwbMmmpnjpfChfQpnhfcCCnH
dFnFjWjTQTFzFWPWPgqhRQRqgVhRqfRqQJ
bStrbpmNGHSrBDmrNBtHBhMVLLqLqVVglrllPVLgPg
tSsbBDmBbbGmmSHDbtmHbtNCjnzscZccjnPcTcdzWcvjswFz
lFCjDhqggMlDvMhFDgqFFzHHwHwwwTpLBwmwqmmpBpwT
GPdPnStGncQGNStZPpBmVZmRmfzTRmVVfL
tWtNdPWzsbtMDCbrCbrjrv
BJHMgLlcMTBLCtbqmMDGppmmMM
ZFPsrrdvwrNvrdNZsvhrrzzRSmJRbJSmbztsmpRSSm
NwhfPZFNdFQPVQdvZFNgjglJLTCQngQWllBcTT
jGlQQvQvpRQRGfnPLfcfGTnP
BMqmdBVBwmFdVMFZdcTPqgLnnggTTLSzPS
FVtMMVcbZVrcZMQCHjHWJJCJDvrW
rPPwVwbpRbbVlllTLCTRqTLL
dNdZssBBCBszHsjhDTQgqLDvlTgDZgll
dSsCNNHMdsdWWWmpGfmPFS
rzCLrsjgZjwcwSZc
wNBNRJpRltHNWWRHBlGlJtRcTZSVBmZDVqZTfBVVTDTVTD
NWPtGJPNGWHvpvtwvWgzQvdvQQzhnsnCvCLM
HHbJhzddMPbPgnDWbZ
BLnjLNvBrrcvvvwnwLrnqrgpPRgRNCWgZDPPpDgRpRWp
jtsBqScStfJQnnVF
QVFSVgQFZZQlQqQSlgQpRppSbRTSTppJJbRpLb
cGwCDwjrnrGvzBzGnwwvDBjnpLbsLTTqRPbsJPMJMWpPns
tcGzrCdtGdQmVZqVNQ
RtTRhncVMTVccShRTctLdfPdJpLPqJhZphHpJs
BzssCmFNWWqWwqwPLH
svzvvsmmFBmsggrGlGMVSMtMRRncSQScRRRl
rmmqrQQwLbbGrrGr
cNJzzzWtWmLCGGbLWWbv
cVtMppchzMBVMcNJcMsRwqZFMlgmggmRgg
mQsQBHFMrbddbRqH
NzhcQNfNNtzvWwZdSrgbrprPrwLbgb
zcJVhTtNNcvcfVZmBmQMGMMljTCmlB
FlldqjSlCgfvPFfvFF
rbnDtVBMbprTsbVVcTDTrpMcmNwgHPgghTmNfLwvfPNLwhdT
drMppdnbbtQDBtbnsBbcrrtbSqSSRCjlQZWllllSRlWRGCCC
nqdCsqbbwdsrHFVJHcwFTc
jPPjtWjPWgRltRLsBRrNpHFDHVFWVVJNNHrD
fgllPGQjBffLjtzSsqvbSSzGvnhS
zsVBzMfHHnzlwwVlqcJJFT
ZzRLvLDzQzTmlWlqWRWF
GbQQvpGvSSpjdQjSQZpQZGLfrgBCsHzrdtCsnfCBHsBgdH
zBLbLWzqqwLMnMZTnHlnsHTvFlFHNT
fjhdcrjjdVdrGSmmdfccGclPvlvPTlGHTFgNvNgqFFvg
pmmcrcRrjSVJchqVccjpRwZMDwCJQBbLDCCbwBWLzL
TDMBgBgLlcjBfMfcVJVmGnnJjvPVCPVv
zzptqHstJqFzzdJJZNvNpvNpnNvGnNZm
dHszrWQhdzHQqdztwQBLSfglfDbfJlJTLg
VTmvrldtGGwmlvmGDHlLnFDCCplFQHLH
ssgjzSzzJCQSSFVVQF
WsRWhgVqRtfvwcddhc
bdlDwznhnNlffMcPTPfzzQ
srCRGRrZCmVTBfBBfTQcZb
brSrrGvRVvWmRsrHrWSbjNJwdDFhnNlwtlnSdnhN
QQqqRfdQQSdjgPmZfBmmPgRhphphJtLmJhTJJhVbTtLhTb
vvlNGzDDDcslcsGDlWHtCFVpcCbThFTtbJFtCh
DrMGlzMVwNGWsWMHDMvlzlMfZdQdQPZfSZRfdrPBfqRZgj
qVHfHNJCHVvvFFbfFlHHnCQQDhLnhhhPZrZnPZPn
mSMszWRMQmhqrnZL
GjtzjSSdRGSjsRtdRMttgGgsqqFNfFcGVvVVvlbHFFGFVFwb";
    }

    
}
