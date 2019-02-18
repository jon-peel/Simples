﻿using Simples.SouthAfrican;
using System;

namespace SimplesCli
{
    class Program
    {
        private const string Jon_Hex = "019b09450000647703fb7a9bdb264e2a14e6cef33dbb05c9f48cac17e550980aa72fd65d456238a7dd6e401de7e985254e5b6fff0adb99384fc2a8ac833b53cdc2c61034af8591d1a0d8ed64c5ea02259a2d957b32cea08fab3f6c8a87e314eed1c7a1ac92f52bac4b9d195a938859bdc3b769079a23306d0e5e97d6d3bda89923dedc2599d35d2cb070a074f61557ffc2001d34c522f98b7b4c02d8a9bbd54118fcd3a697d968af72defaa4b56b22fbbbcb46f9dbb345a97f61ac0c12a3b7a0161c38c2956fa6902010ba770605e0754cf22603eb62009b2fa503735ae5dffadf844160ed10967eb4ada4f18b4d46a5875e0115c844f508f2606ec83317717f4eb53c8c85a96061fce437f737333d7066c5bf68bfa480094ead9e8de475fd439ec9b8dc8d622c849892375adb97deb98df4a89bda32baa2d27dce8071594f9205bb3d948a0fdc2bb19cfdf81a4d8a2a6c0c679d1fc5448c65f85d4ca0a4a4fe767f097349d074a974a732a25ce20f58c22c41ccf340e345d48e0963af01d199fd6c9d92f286a109f2ac79d5d8b78e34e31d44af042c4123c152dcc37b82dc7742cd971e74384a764e3f321344a08823538815e5977a2145f83b7a884ffe8b508cfb8ef2d5b66bdb156b6a891a6a2db2e041087a934d0f351f1c0fa1e25532833f367a7fc9eb89bec41cc9ba685fa6a616844cbb637d05f96b3453f4df87237f5d694e1bded5c96963d942cd6c2a40fe2095e27a48284dffffb9a2909e48f22832dd89daeca13ee0fcfd777f33e21a80fe47b0eb24d68ad379f61475929544bb557aa747f0528e82c1eb3dfb120b5ea75d8b456a12e4a87d7364c7acb28ecb8e8cdf10df29228e963c9142ca5c834b240c13ee9e813ee075ece819a6f64c89967ef0c3c3a27f574fa1b675b00e71cda6d3ccf68bbf6c85cd04aea394375e4b40d262d5a6770282f898ba69a4dfa0b9ebe9a6a703498244056fb7d1b7e2ef075eb4b1b70070e695097c56bb5bbd3bb971";
        // private const string sampleData = "Am0WAwAyARaCWkLh4eFTQU5ERVJT4ErhWkHgWkHgMOHh4TYyMzQ1NjAwMDFBQuA4NjA5MTM1MTM5MDEyAiAHAhmqoQoBGYYJEyASERQgFxIUAVdJBAD6AMhDLihAAChCNFWnQrzo0jDnNeVd2987O9NM9v45zkHm/ll7Ivb5cBQx7vfrFr3e975/m97of/HDQKGCh/LRixwt8OqE5KhYXoNDmoVPDQKFWdbMIXtUo8+40zll0RmpShJw785Ys/luDajQ1gRRksEkr7FjiKHRMO4wpyFBGMJfagh7iuNxHOEMJFrGOBpRg5UETHrDTx0mx9RmWuHQjFA+Naa7abq64z0qL5B3ktR5oFuN4rtWXoZeql6MiPthbIY+vGEUN5mPczaChm0UvGwwkQckiCBP+LSAAVexEcEgAKywW4F+62PhksQeG6Zx/0fCjSQUBWD4fEwVwSnz5ARYJAVimcsOERK2EkQ0kXYSqR2NGSxZ9JY1ADb8Y2aC1hQPh/F/NXq+j/IVV3ratkIa1ETAaPu91/FhMgl1MdUWp5yn8UVVm+wFiWW1IC/UsvkUOkKHhSYCaMqt5loAOXGqumWXwtoOK4QcqHboj8RwAqatEPFrOGDu9ChjRI9HpjPpgvTsB3VGApj8Q8RXRPC8zi097mSBVqhNcGcO2vSTKjjIPC5yG+ISRwvFBBnEW9tarAR3lZ98IAMIQgu0VTRca4lsqeIB8xjXXuw1l24bRDiPYi5JEwAlWO09CP+G+6jYdgVLBYskCWGukFDX2g2NLBGjvYB47rCucJ8Yhgjdik1EAgNiKFN3KCCS6qCixBMkBqrbd0FnPWnX3eYFjOWqLbVoEAUARFpF1RRFEBRZS4YgoqogABY7UAEAFFAFVVUBBEVAVVQQAAAAAAAAAAAAAAAA";

        static void Main(string[] args)
        {
            var dl = DrivingLicence.FromHex(Jon_Hex); //.FromBase64(sampleData);
            // var dl = DrivingLicence.FromBase64(sampleData);
            Console.WriteLine("JM", dl.Initials);
        }
    }
}
