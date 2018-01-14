﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Server.Cryptography
{
    public static class RSA
    {

#region "Public Properties"

        public static int KeySize { get { return 2048; } }

#endregion

#region "Private Fields"

        private static byte[] E = new byte[] { 1, 0, 1 };
        private static byte[] M = new byte[] { 218, 109, 48, 177, 179, 250, 157, 248, 64, 185, 207, 71, 204, 110, 113, 191, 252, 249, 68, 16, 21, 255, 160, 38, 7, 174, 170, 235, 16, 28, 219, 181, 24, 107, 123, 75, 128, 207, 106, 109, 13, 237, 161, 44, 32, 45, 184, 232, 41, 176, 91, 95, 109, 170, 207, 119, 117, 184, 37, 228, 29, 127, 157, 134, 96, 225, 34, 178, 241, 224, 54, 105, 140, 250, 95, 25, 128, 12, 174, 146, 23, 67, 132, 116, 2, 1, 205, 181, 254, 25, 97, 197, 109, 102, 227, 198, 59, 245, 188, 56, 61, 67, 9, 162, 238, 161, 180, 65, 112, 96, 52, 28, 62, 216, 148, 10, 12, 0, 28, 147, 203, 192, 208, 10, 148, 80, 91, 147, 3, 126, 5, 234, 193, 238, 65, 249, 12, 107, 249, 122, 125, 138, 176, 78, 111, 44, 92, 167, 158, 137, 13, 88, 174, 236, 163, 94, 199, 231, 122, 233, 229, 179, 191, 112, 97, 230, 247, 3, 0, 24, 104, 140, 152, 183, 68, 217, 113, 150, 144, 248, 225, 89, 50, 36, 90, 231, 178, 31, 21, 127, 188, 75, 103, 194, 117, 25, 10, 185, 174, 36, 203, 25, 168, 130, 251, 80, 54, 136, 40, 72, 60, 216, 206, 101, 133, 235, 182, 167, 89, 229, 109, 214, 113, 216, 229, 139, 238, 128, 68, 197, 131, 240, 172, 143, 251, 238, 234, 160, 145, 89, 210, 137, 250, 187, 215, 226, 164, 104, 157, 76, 109, 161, 62, 59, 173, 193 };

        private static byte[] Q = new byte[] { 244, 21, 164, 8, 20, 169, 129, 168, 100, 202, 25, 98, 48, 204, 215, 247, 75, 4, 75, 229, 96, 137, 252, 38, 238, 248, 9, 20, 19, 94, 176, 121, 83, 156, 241, 60, 236, 241, 249, 91, 141, 102, 204, 120, 166, 168, 173, 235, 96, 236, 12, 182, 193, 87, 221, 159, 20, 152, 195, 184, 49, 221, 231, 251, 246, 97, 120, 152, 207, 23, 125, 50, 103, 219, 64, 42, 110, 234, 145, 13, 245, 52, 227, 126, 15, 43, 118, 10, 168, 223, 251, 220, 180, 136, 107, 24, 120, 34, 163, 255, 146, 32, 113, 23, 114, 46, 97, 185, 9, 120, 99, 20, 37, 146, 26, 161, 238, 127, 178, 1, 208, 207, 166, 235, 188, 18, 71, 15 };
        private static byte[] DQ = new byte[] { 146, 196, 60, 120, 187, 107, 221, 81, 211, 33, 14, 184, 83, 29, 169, 50, 194, 129, 215, 221, 25, 250, 126, 139, 46, 160, 40, 93, 80, 244, 91, 234, 232, 82, 243, 95, 174, 30, 37, 209, 94, 42, 21, 2, 235, 59, 124, 48, 143, 124, 0, 125, 23, 174, 236, 205, 14, 6, 121, 145, 85, 50, 216, 158, 81, 220, 244, 53, 246, 56, 106, 136, 151, 152, 103, 113, 220, 117, 225, 56, 226, 91, 165, 251, 19, 198, 15, 89, 131, 136, 3, 27, 7, 215, 11, 134, 144, 30, 76, 60, 141, 178, 66, 46, 85, 242, 251, 193, 214, 166, 243, 244, 150, 78, 207, 2, 213, 181, 36, 229, 32, 152, 236, 6, 205, 116, 180, 19 };
        private static byte[] IQ = new byte[] { 171, 49, 52, 179, 105, 186, 255, 131, 42, 90, 13, 34, 155, 211, 195, 11, 244, 119, 96, 41, 109, 158, 241, 123, 52, 119, 214, 38, 46, 138, 0, 33, 163, 125, 57, 51, 189, 83, 54, 111, 40, 199, 19, 142, 128, 118, 7, 224, 112, 110, 20, 21, 183, 9, 5, 8, 243, 18, 12, 109, 27, 244, 84, 205, 4, 205, 150, 203, 179, 157, 7, 218, 112, 192, 107, 135, 112, 43, 153, 71, 187, 219, 60, 158, 202, 164, 67, 202, 127, 123, 253, 93, 41, 0, 55, 155, 31, 128, 171, 139, 189, 181, 80, 142, 63, 160, 167, 205, 153, 85, 24, 153, 218, 184, 18, 104, 193, 151, 247, 20, 61, 114, 130, 177, 132, 119, 188, 198 };
        private static byte[] P = new byte[] { 229, 22, 229, 204, 138, 66, 181, 246, 238, 32, 198, 242, 77, 86, 101, 252, 35, 223, 104, 16, 144, 137, 135, 12, 14, 159, 137, 1, 182, 15, 202, 96, 148, 187, 218, 246, 134, 193, 44, 161, 168, 252, 243, 195, 128, 32, 140, 133, 219, 133, 32, 22, 228, 224, 70, 235, 106, 66, 83, 117, 185, 137, 160, 48, 176, 32, 158, 10, 129, 99, 24, 65, 207, 42, 175, 144, 147, 137, 18, 95, 153, 172, 1, 166, 28, 173, 246, 169, 232, 255, 178, 38, 145, 240, 238, 139, 135, 139, 179, 93, 103, 46, 222, 23, 238, 137, 197, 86, 118, 45, 159, 150, 140, 221, 3, 74, 116, 105, 127, 45, 180, 15, 105, 74, 179, 165, 62, 47 };
        private static byte[] DP = new byte[] { 60, 41, 142, 65, 240, 17, 98, 238, 45, 77, 72, 81, 251, 195, 115, 215, 10, 168, 178, 16, 159, 148, 174, 117, 153, 37, 134, 122, 127, 144, 152, 10, 83, 37, 30, 116, 221, 160, 191, 146, 216, 233, 77, 47, 11, 104, 0, 223, 106, 110, 4, 166, 94, 135, 19, 184, 225, 87, 247, 201, 19, 231, 179, 188, 245, 148, 43, 77, 0, 251, 192, 52, 16, 46, 218, 154, 114, 84, 110, 17, 211, 46, 155, 228, 62, 229, 228, 192, 108, 99, 50, 42, 244, 234, 188, 10, 194, 151, 10, 140, 189, 251, 77, 242, 36, 255, 227, 102, 56, 116, 244, 211, 57, 189, 21, 15, 41, 145, 71, 7, 254, 160, 137, 123, 71, 192, 94, 155 };
        private static byte[] D = new byte[] { 28, 115, 12, 92, 152, 236, 147, 150, 186, 23, 121, 128, 8, 136, 122, 24, 95, 130, 183, 242, 192, 106, 195, 25, 48, 246, 247, 28, 22, 197, 43, 120, 178, 237, 140, 233, 144, 165, 115, 95, 48, 233, 58, 140, 220, 196, 23, 201, 101, 100, 171, 2, 10, 238, 172, 135, 10, 182, 41, 121, 68, 190, 82, 110, 38, 64, 44, 156, 210, 20, 58, 2, 21, 114, 166, 224, 83, 101, 83, 119, 131, 192, 47, 136, 76, 154, 159, 136, 140, 200, 17, 139, 253, 107, 225, 78, 248, 144, 81, 43, 162, 15, 17, 58, 63, 138, 160, 71, 197, 179, 66, 22, 75, 185, 57, 206, 245, 49, 46, 135, 177, 138, 51, 142, 173, 50, 170, 58, 134, 32, 134, 224, 251, 62, 80, 202, 206, 246, 153, 167, 241, 84, 47, 248, 218, 88, 156, 96, 72, 15, 127, 38, 217, 170, 18, 206, 71, 22, 203, 131, 73, 164, 134, 249, 240, 172, 159, 20, 249, 232, 165, 227, 148, 115, 68, 183, 189, 244, 88, 207, 168, 171, 237, 113, 148, 207, 175, 126, 169, 212, 237, 52, 244, 70, 198, 6, 223, 115, 72, 107, 247, 93, 122, 59, 209, 150, 41, 50, 213, 184, 56, 28, 54, 239, 184, 121, 14, 57, 83, 145, 233, 62, 158, 224, 132, 240, 15, 183, 113, 36, 188, 74, 80, 238, 147, 179, 101, 227, 172, 161, 242, 180, 140, 254, 24, 102, 69, 51, 178, 146, 204, 16, 21, 69, 241, 97 };

        private static RSACryptoServiceProvider rsa;

        private static bool isInit = false;

#endregion

        private static void _init()
        {
            rsa = new RSACryptoServiceProvider();
            RSAParameters rsaParams = new RSAParameters();
            rsaParams.Exponent = E;
            rsaParams.Modulus = M;
            rsaParams.Q = Q;
            rsaParams.DQ = DQ;
            rsaParams.InverseQ = IQ;
            rsaParams.P = P;
            rsaParams.DP = DP;
            rsaParams.D = D;
            rsa.ImportParameters(rsaParams);
            isInit = true;
        }

        private static void _checkInit()
        {
            if (!isInit)
                _init();
        }

        public static byte[] Encrypt(byte[] value)
        {
            _checkInit();
            return rsa.Encrypt(value, true);
        }

        public static byte[] Decrypt(byte[] value)
        {
            _checkInit();
            return rsa.Decrypt(value, true);
        }
    }
}