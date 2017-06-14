using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Flood_Reg
{
    class Program
    {
        // TCP Flood 공격 대응
        static void Main(string[] args)
        {
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\", true);

            if (reg != null)
            {
                // Half-open 상태의 타임아웃 값을 짧게 설정하여 Half-open을 통해 자원 고갈되는 것을 방지
                reg.SetValue("SynAttackProtect", 1, RegistryValueKind.DWord);

                // 몇 개 이상부터 연결 요청을 거절할 것인지를 지정, 만약 0을 주면 백 로그로 이용
                reg.SetValue("TcpMaxPortsExhausted", 5, RegistryValueKind.DWord);

                // Half-OPen 상태로 유지할 수 있는 최대 개수를 지정
                reg.SetValue("TcpMaxHalfOpen", 500, RegistryValueKind.DWord);

                // 대기 상태의 연결 소켓에 대한 최대 재시도 개수를 지정
                reg.SetValue("TcpMaxHalfOpenRetried", 400, RegistryValueKind.DWord);

                //  재 연결 시도 횟수가 2번으로 조정
                reg.SetValue("TcpMaxConnectResponseRetransmissions", 2, RegistryValueKind.DWord);

                Console.WriteLine("레지스트리 설정 성공!");
            }
            else
            {
                Console.WriteLine("레지스트리 설정 실패!");
            }
        }
    }
}
