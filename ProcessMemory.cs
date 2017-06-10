using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SLWResolutionEditor
{
    // I see that you want read ProcessMemory
    public class ProcessMemory
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, UIntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);


        public static int WriteBlock(IntPtr handle, uint address, byte[] block)
        {
            WriteProcessMemory(handle, new UIntPtr(address), block, (uint) block.LongLength, out int bytesWritten);
            return bytesWritten;
        }

        public static byte[] ReadBlock(IntPtr handle, uint address, uint size)
        {
            var @out = new byte[size];
            bool a = ReadProcessMemory(handle, new UIntPtr(address), @out, size, out int wtf);
            return @out;
        }

        // Read Methods
        public static uint ReadUInt32(IntPtr handle, uint address)
        {
            return BitConverter.ToUInt32(ReadBlock(handle, address, 4), 0);
        }

        public static ulong ReadUInt64(IntPtr handle, uint address)
        {
            return BitConverter.ToUInt64(ReadBlock(handle, address, 8), 0);
        }

        // Write Methods

        public static int WriteInt32(IntPtr handle, uint address, int value)
        {
            return WriteBlock(handle, address, BitConverter.GetBytes(value));
        }

        public static int WriteUInt32(IntPtr handle, uint address, uint value)
        {
            return WriteBlock(handle, address, BitConverter.GetBytes(value));
        }

        public static int WriteInt64(IntPtr handle, uint address, long value)
        {
            return WriteBlock(handle, address, BitConverter.GetBytes(value));
        }

        public static int WriteUInt64(IntPtr handle, uint address, ulong value)
        {
            return WriteBlock(handle, address, BitConverter.GetBytes(value));
        }

    }
}
