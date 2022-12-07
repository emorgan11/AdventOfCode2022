using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    class TuningTrouble
    {
        public TuningTrouble()
        {
            Console.WriteLine("\r\n\r\nTuning Trouble, GO!");
            Console.WriteLine("----------------------------------");
        }

        public void FindMarker(int numChars)
        {
            Queue<char> buffer = new();
            int num = 0;
            foreach(var a in input)
            {
                buffer.Enqueue(a);
                num++;
                if(buffer.Count == numChars)
                {
                    //search for dupes
                    if(findDuplicateMarkers(buffer))
                    {
                        //We found a dupe, back down to 3. try again
                        buffer.Dequeue();
                        
                    }
                    else
                    {
                        // we found the start of the packet marker!
                        Console.WriteLine("Num chars before first marker: {0}, with {1} distinct characters", num, numChars);
                        return;
                    }
                }
                
            }

            
        }

        private bool findDuplicateMarkers(Queue<char> buffer)
        {
            int bufferIdx = 0;
            foreach(var a in buffer)
            {
                for(int i=0;i<buffer.Count;i++)
                {
                    if(i == bufferIdx)
                    {
                        continue;
                    }
                    else
                    {
                        if (a == buffer.ElementAt(i))
                        {
                            return true;
                        }
                    }
                }
                bufferIdx++;
            }
            return false;
        }

        private static readonly string input = @"bfdbbngnvnsvshhhrvrbrtbrrhqrqgrrmmdfmmqttptltntrntrnrcrdcrrctctdtwtrwwmlltcltcllmpprvvtbbmbvbsvvqwvvscswsqqgzqqppvzppnddjwddlrdlllmwllfccdfccswwrhhndhdhfdftdfdcdcllcjjbsbgssvlvrrhfrfjfpjffvnfvfwfzftztwwrhwrhwhwddpjjmhmgmsssstzszqzfqzzprpzpnndttphtpphvvcpcjppdtdwwqgwwwnhwnnvhvsvqqtrrbsscwssgwglgwlldjdzjjbsjbsbvvpbvppvfvqvbqbzzwtwbwpbbbcffdgggpnpfptpfphfhfthfthffzrrbzbcblbmmzmhhnvvqvlvwvzvtztgzznbbcqbccwhcwcnwnhndhhgcghhlthllplvppgngrgsswfsfnfjjswstsrtrhthhqzqggdtggzvvjljddqdzzrmrjmjcmjmrrwlldttrhhfjjgbglgjlldrllppnwpnwnndrddtdqdhhpccbgcgzzswzztgzzrwwzzmtztvvrtrgttvddwvwvmwvmmdjdrdtthqhgqhggldgdfdnnzjnjffmrrnprnpnssjrrbrjjrrzvrvhvllfvlvnlvvhlhrhzrzrsrswrssjvjdjfjwwjmjvjjthhnjnmnppsccmlcclfclcbllldwldwdlwddbddmnmrmgrmmtsszgssqjsqscswwzmwmbwwvqqbtbtwwpbbwdbdbhhqmqgqddflfmfnmffbtfbbsdsffspsjpsspzszccrsrstrtjrjppcrrpqqvttsbbjtbjjpbbccmzznvvzzvnvmmwmhmvmsstlssqtssvggtbgttgvtggfhggbcgcmcrmcmhhbttdlttfrfprrvttrtggcpplccqggcwwdjjjplprrbsrbbjsjbbfhbhphtttpffdgdgjddzldzlzplpnplljttdqqlmlllhzhjhccpwcwhwwbbqtqffjpfpfpjptplltbtzbbwwvggpcpprdprpbpvvgzgczzfffzcfzccjffrmrhmrhhchvhddsvswscsnnpfppdrdcrdrgdgmdgmgllhggjwggvqvmmvrrzpprhhqfhfvffvlvnlngnffbbtccfbfhfwhhhhwcwzwjjbffvrffqrrnsnwswrsrvrjvrvprpsptspttbztzfzlfzlflcflclqlplglmlqqvlvvhccmmddqvqzvvmlllvnlnhnfnwnlndnntrrhvvgjjnpnggfqgqdqqzvvclvclvlfvlvvlzlssqffmrfmfmjjczcmmmnttnfnmfnmmmhmggjgdjjqmjjvsslszlzrrpdpcpfcpfpnpssvtsvtvllbttzzljzzhrrnsnddjgdjgjljzzvhvddmdvvdtdmmpmqmnnwcwfwbfwwsvvbpvvgvnvcvdvtvrrcppwbppcqpccwqqzmqmmzczppwbbrzrjzzcrcvrrpttrjjcdjcjfcclscstthqhfqhffdcfdfqdqldddsswbblppdmdnnjvvhwvhvjhvjhjhlhcclrrcqcgcjgcjjtbtctbcbsccfwwltlrlplpfpjphplhhgtghhrppwhhrprwpwhhdqdpdwpwwtccvncvvvrpvphpssfpplcczttltgltldlmlqmmbsszdszznpprsprsprsppsbppjddjhhnrrfsrffmlmglgmlglccddpssdpsddhhfmfsfrfsfnnlggfrgghmgmrmprmprrnvrnnbqnqddhhfwfmfwfmfpfdfzdzppsbpsbpprfrvvwvtwtltvlttrhhgvhhjttmssdggnzzvmvcczmzbzwzttvtpvpcphcctmmhshjsjbssglsglgtltslscchhhsjjpljjdtdsdpppptlpprmppwdppglgmggnddztdzddzppswwmhfbpqzffjqgmsntwsnrwqrqwgpwgrbpbjwrhbcdcvqjnwslsnwhglcsjbwjhswjvzssfqgwbbdgbwfrblfmmlmsndhtlbwzfwsspqlncspqbgbnzshbwpvrmjqjzbcbzzdgssbtqdzffjphqjvrspfrjhpspbwcjwbfhqzsdnjwqjzjtjgnbrdbwqhzffphzppvlmsmppqcfjbjbdsnbwtvthwqcfrtfrwchnmqmhnwfcjtbwqwwvlnpmwlrvzwljrljzqstzglqwbzfdftzltlcbvmmfwcjqglvznztwnvzvftpndqmngqswppsnqhdbgthrddfbcfpflpndrhmcqwvnbfztsvnjjdwqgpmvdwvdftgbtvrwbnvvrwsdfzhwbwdhlpzbcqdzhbfqtpjqcmrpvcsrmcwvgghqrclfzpfgnppzmhvdhvdfrcrnjbdcwbftcqjhhfdsnfnwjzjllzzqftzsjrqnsbpjdcswhhmwwdzmvmqcjtqnczjcvzmmqwzjhjpcczgpbmcvbwmpmvnghlrgcmrrdnmjvmvnhtpfpgwgmdfzvlbclzjzwdqqcvfhhgfzdhzpdvfmwjlzzrpdgzmttmvvcplbwfzqftcgcwcgcpgwvnmlqsplpqwfnhwvqtlwcspqdzshsqnlcpqhpcpbwdhdjsmvtbqdwbcrscqfjcrcjhbjbpzbshpbmlcthmbjfwhzfphgbfqfnfztptzvdnwrpslmdtpmzmpbsszqshwdghrbtvhwzhcmpcgfqggpgzwmhhdrlhlvnpzvwwhzqvgvhrzngttcnqgjjhnblncnqnjzlwnmwnrtvwjtnrbhthncwmzwqdbdgtwrncljddnbhmphgjzfrrgmmcwfwjwjlcrhvcdtvsrvsfhmlgmzsgjchhrfmqslmdgtdtrlbhdffddvbsdbdwlwdcmcmmpvzpdtmbthjdzlpwftptpfsggmhjfjwvbwljsfhfwtbfwmczwhbmhvzllqtcqqfbrcdqqsrcpfmnswnfzfqghcmcbqwgvzqpwvvmbpddlhgjgzvgmpljznrhqphwcztqzpnhzqdgpwwclmsgpwnwtvtsjsdmcnvmjbqglttrhbzqdbgwnbsqzmsmztndrtmlpszhzgjbbftbsdwwdrlftrbbnrsqshfhdpdrwmztcqzdjlnthnjhppwntmbqdgzpmfmfnccblsdwljqhjfgtlgvpzpjbsndmwzfwrbmdhpnmbchqlwqtbhhhqqbsfnvscjwrzvjdtvbsqwzvfhwbbjgqpzcwqjdrlfmggzmhbcrhtbqdjntbtqdvmvpqflmccfpnbmnmtqbdflsgczpbsqpfphlzqgvwbjlmsgshrhpcljzdvwvdvlqqwchtjmjgtqjhgwtnddmhphwhvwhtrhfbjjjzfgrcqngnnddctzdzlqjlbdwmjqzccwrvctrzgtzqsswggbqdnplclhtdslcvzhppcjjslnshtwjnbrwdprqhdtfqmqpgfgnqtdnnhrnzfrsqhlftpdslgmmvqhvpjqjwpwgtnmgrbhwntdjftfwtjzjtprctbtsjmqmpcbbtrjvsgqgsfjprqmsmdztbhnbgzldqfzgwqwnnccgcfclctrwqmqpgvfglgsmmpjszqnphnzcnvswpsfsrmnsnlqnpmvfdvdtfgzrdmbftdrrrbfsvzfgmnffvjpcpnndrwhtjjrrvnztlfhcvfqjgfrhtbnhmwnrmwdhzmmtvjmsqmghtbtfjwdnvdcqtqjrfhrwscjftmbgjmcsrbpdpttlmvfmnfjhnptqvggnshzqnlqqdpqqsqssppbwpblhgfrwrblpzwvqphpsgfmbpqtqqpjpgnbblzstgcjhqntgpbfwlzzctqbnbvpgwsdsdldqzhvznqcsrrghpwllshqpdlqnqgzfwrnhwsvhftzplspcbqmclplprlthvwjhdndrjblqdgwvgjlbmblbmcnbzwzdlnpnhhppvrtngvqqwsttgwlvtcqmtrvpbnvcnfqdtqrsrsmhclmtgbdwwdvhwgfcqpmprcpdhqwftcchbwvstcdqrlwtgbcfqfgzprgvpbbzlqfzbqtcrlzscnqpqwtgzbbbdvsvmhggdr";
    }
}
