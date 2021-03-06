﻿#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 973. K Closest Points to Origin
// Url: https://leetcode.com/problems/k-closest-points-to-origin/

namespace L973
{
    public class Solution
    {
        public int[][] KClosest(int[][] points, int K)
        {
            // SS: runtime complexity: O(N + K log N)
            // Note: If we were to only add up to K elements to the heap,
            // we could get runtime O(N + K log K)
            if (points.Any() == false || K == 0)
            {
                return new int[0][];
            }

            // SS: add priority to we can use Floyd's algorithm
            // to make a heap in O(N) (instead of O(N log N))
            var data = new (double, int[])[points.Length];
            for (var i = 0; i < points.Length; i++)
            {
                var p = points[i];
                var distance2 = p[0] * p[0] + p[1] * p[1];
                var distance = Math.Sqrt(distance2);
                data[i] = (distance, p);
            }

            var pq = PriorityQueue<int[]>.CreateMinPriorityQueue(data);

            // SS: runtime O(K log N)
            var result = new List<int[]>(K);
            var count = 0;
            while (pq.IsEmpty == false && count < K)
            {
                var p = pq.Dequeue();
                result.Add(p.value);
                count++;
            }

            return result.ToArray();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var points = new[] {new[] {1, 3}, new[] {-2, 2}};
            var K = 1;

            // Act
            var result = new Solution().KClosest(points, K);

            // Assert
            CollectionAssert.AreEquivalent(new[] {new[] {-2, 2}}, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var points = new[] {new[] {3, 3}, new[] {5, -1}, new[] {-2, 4}};
            var K = 2;

            // Act
            var result = new Solution().KClosest(points, K);

            // Assert
            CollectionAssert.AreEquivalent(new[] {new[] {-2, 4}, new[] {3, 3}}, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var points = new[]
            {
                new[] {-9, 7}, new[] {-5, 3}, new[] {-5, -8}, new[] {-2, -8}, new[] {1, -5}, new[] {10, 3}
                , new[] {8, -8}
            };
            var K = 6;

            // Act
            var result = new Solution().KClosest(points, K);

            // Assert
            CollectionAssert.AreEquivalent(
                new[] {new[] {1, -5}, new[] {-5, 3}, new[] {-2, -8}, new[] {-5, -8}, new[] {10, 3}, new[] {8, -8}}
                , result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var points = new[]
            {
                new[] {-790, -327}, new[] {152, -835}, new[] {-183, 523}, new[] {113, 812}, new[] {671, 564}
                , new[] {650, -240}, new[] {-169, -996}, new[] {809, 352}, new[] {-470, 273}, new[] {514, 939}
                , new[] {821, -875}, new[] {632, 569}, new[] {22, -256}, new[] {145, 778}, new[] {-950, -811}
                , new[] {445, 27}, new[] {-725, -705}, new[] {725, 347}, new[] {-528, -12}, new[] {-298, 411}
                , new[] {923, -615}, new[] {860, 666}, new[] {160, 984}, new[] {-523, -971}, new[] {572, 497}
                , new[] {492, -668}, new[] {-358, 901}, new[] {641, -227}, new[] {-686, 703}, new[] {-248, -483}
                , new[] {365, 764}, new[] {485, 990}, new[] {-417, -392}, new[] {840, -737}, new[] {600, -843}
                , new[] {-472, -671}, new[] {-143, -398}, new[] {904, 20}, new[] {-183, -327}, new[] {-485, -117}
                , new[] {866, 732}, new[] {-269, 982}, new[] {-371, -354}, new[] {-619, -348}, new[] {-369, -610}
                , new[] {696, 165}, new[] {-286, -76}, new[] {-329, -165}, new[] {675, -585}, new[] {938, 119}
                , new[] {-903, 876}, new[] {-503, 403}, new[] {-989, 855}, new[] {872, -244}, new[] {584, -350}
                , new[] {-570, -519}, new[] {-668, 286}, new[] {-209, -993}, new[] {-683, -822}, new[] {484, -104}
                , new[] {-820, -304}, new[] {389, 594}, new[] {-252, 771}, new[] {703, -962}, new[] {-213, -596}
                , new[] {811, 385}, new[] {-384, -547}, new[] {116, -241}, new[] {-229, 382}, new[] {906, 761}
                , new[] {945, 523}, new[] {325, 77}, new[] {-875, 965}, new[] {267, -380}, new[] {595, -222}
                , new[] {-719, -178}, new[] {-800, -920}, new[] {653, -344}, new[] {993, 711}, new[] {-134, 172}
                , new[] {-591, 447}, new[] {-319, 546}, new[] {311, 388}, new[] {-3, -485}, new[] {316, -64}
                , new[] {-983, -28}, new[] {-239, -764}, new[] {-753, 801}, new[] {-784, 79}, new[] {526, -77}
                , new[] {-411, -346}, new[] {-952, -907}, new[] {-10, 488}, new[] {-165, 646}, new[] {688, 21}
                , new[] {-921, -511}, new[] {708, -997}, new[] {-579, -227}, new[] {-872, 376}, new[] {31, -77}
                , new[] {344, -258}, new[] {891, -690}, new[] {48, -887}, new[] {651, -953}, new[] {158, 161}
                , new[] {-603, -376}, new[] {-642, -110}, new[] {-905, 287}, new[] {518, -821}, new[] {352, -372}
                , new[] {133, -404}, new[] {813, 110}, new[] {292, 88}, new[] {-195, 286}, new[] {-612, -30}
                , new[] {-144, -722}, new[] {-409, 617}, new[] {-110, -315}, new[] {-527, -897}, new[] {-736, 890}
                , new[] {-981, 856}, new[] {858, -820}, new[] {-375, -84}, new[] {-537, -142}, new[] {324, 413}
                , new[] {666, -186}, new[] {177, 415}, new[] {110, 523}, new[] {611, 276}, new[] {105, -21}
                , new[] {155, -6}, new[] {14, 809}, new[] {-897, -592}, new[] {-407, 151}, new[] {751, 698}
                , new[] {-710, -275}, new[] {-864, -137}, new[] {64, 762}, new[] {-970, -289}, new[] {911, 151}
                , new[] {315, 617}, new[] {87, -315}, new[] {-840, 283}, new[] {-814, 238}, new[] {-580, 667}
                , new[] {527, -947}, new[] {369, -376}, new[] {-132, 948}, new[] {997, -232}, new[] {407, 180}
                , new[] {94, -531}, new[] {563, -105}, new[] {-956, 731}, new[] {227, 367}, new[] {703, 129}
                , new[] {691, -139}, new[] {-119, 797}, new[] {-234, -512}, new[] {287, 617}, new[] {-391, 302}
                , new[] {695, -371}, new[] {-243, -10}, new[] {-25, -164}, new[] {-352, 675}, new[] {142, 599}
                , new[] {-79, -995}, new[] {808, -834}, new[] {-547, 672}, new[] {-707, 334}, new[] {-692, 205}
                , new[] {411, -328}, new[] {-919, 603}, new[] {-961, -103}, new[] {453, 79}, new[] {-855, 22}
                , new[] {390, 54}, new[] {-842, -862}, new[] {-211, 752}, new[] {-170, 886}, new[] {512, -203}
                , new[] {-304, 799}, new[] {-543, 906}, new[] {-761, -625}, new[] {-815, -330}, new[] {-872, 156}
                , new[] {860, -798}, new[] {868, 788}, new[] {28, 600}, new[] {486, 827}, new[] {858, -200}
                , new[] {-98, -421}, new[] {-866, -131}, new[] {707, 636}, new[] {935, -95}, new[] {593, -382}
                , new[] {-624, 457}, new[] {-4, 955}, new[] {453, 237}, new[] {976, -108}, new[] {-208, 274}
                , new[] {-582, -429}, new[] {578, -874}, new[] {-340, -421}, new[] {653, -996}, new[] {-100, 982}
                , new[] {-747, -212}, new[] {-238, -124}, new[] {802, 476}, new[] {99, 251}, new[] {316, 465}
                , new[] {402, 538}, new[] {47, 424}, new[] {-343, -325}, new[] {-642, 922}, new[] {321, 747}
                , new[] {478, -504}, new[] {-348, 218}, new[] {-437, -325}, new[] {-326, 157}, new[] {-116, -629}
                , new[] {-601, -241}, new[] {-416, 928}, new[] {450, -150}, new[] {-495, 190}, new[] {-780, -546}
                , new[] {844, 78}, new[] {700, -576}, new[] {-955, 798}, new[] {-674, -736}, new[] {824, -818}
                , new[] {-587, -634}, new[] {759, 447}, new[] {839, -448}, new[] {-486, 470}, new[] {-210, -755}
                , new[] {-792, 637}, new[] {975, 110}, new[] {359, -253}, new[] {-745, 908}, new[] {696, -113}
                , new[] {18, -58}, new[] {315, -893}, new[] {372, 64}, new[] {-214, 192}, new[] {838, 288}
                , new[] {555, -615}, new[] {-979, 26}, new[] {-19, -257}, new[] {-272, -866}, new[] {723, 798}
                , new[] {111, 886}, new[] {-930, 451}, new[] {169, 884}, new[] {535, -320}, new[] {-822, 406}
                , new[] {-586, -609}, new[] {-228, 21}, new[] {-533, -839}, new[] {-131, 882}, new[] {610, 535}
                , new[] {236, -236}, new[] {-379, 955}, new[] {55, 368}, new[] {-983, -908}, new[] {-534, 424}
                , new[] {-378, 696}, new[] {-777, 92}, new[] {-188, -551}, new[] {-942, -748}, new[] {-185, 247}
                , new[] {-68, 550}, new[] {-738, -272}, new[] {178, 182}, new[] {965, 805}, new[] {-291, 744}
                , new[] {109, 616}, new[] {-764, 987}, new[] {-647, -7}, new[] {807, -299}, new[] {-259, 499}
                , new[] {884, 162}, new[] {977, 65}, new[] {970, 74}, new[] {-190, -405}, new[] {502, 309}
                , new[] {161, -675}, new[] {954, -916}, new[] {-694, 388}, new[] {-612, -529}, new[] {547, -416}
                , new[] {-795, 113}, new[] {590, -85}, new[] {-774, -67}, new[] {-136, 140}, new[] {-995, -587}
                , new[] {494, -812}, new[] {-845, 425}, new[] {517, 100}, new[] {-97, -786}, new[] {-13, 391}
                , new[] {-227, 231}, new[] {536, 122}, new[] {-650, -869}, new[] {199, -371}, new[] {-845, -650}
                , new[] {222, 101}, new[] {-580, -85}, new[] {-755, 540}, new[] {-177, -339}, new[] {-673, 385}
                , new[] {10, -50}, new[] {-406, 237}, new[] {-280, 751}, new[] {-417, 868}, new[] {534, -210}
                , new[] {931, -106}, new[] {-44, -830}, new[] {155, -753}, new[] {-109, 170}, new[] {50, 270}
                , new[] {767, -558}, new[] {238, -861}, new[] {-665, 815}, new[] {504, -648}, new[] {-248, -18}
                , new[] {561, 355}, new[] {393, -788}, new[] {-366, -188}, new[] {-941, 94}, new[] {-85, 742}
                , new[] {242, 737}, new[] {903, -114}, new[] {141, 918}, new[] {-337, -307}, new[] {-611, -244}
                , new[] {890, -198}, new[] {479, -760}, new[] {332, -194}, new[] {-466, 906}, new[] {-858, 667}
                , new[] {-27, -355}, new[] {-10, -333}, new[] {-196, -370}, new[] {-682, -142}, new[] {-791, 213}
                , new[] {-999, 846}, new[] {490, -161}, new[] {-910, -374}, new[] {-800, -206}, new[] {857, -507}
                , new[] {-721, -824}, new[] {-890, -521}, new[] {-684, -25}, new[] {-373, -181}, new[] {418, -786}
                , new[] {935, -988}, new[] {-615, -396}, new[] {-799, -949}, new[] {-757, 193}, new[] {-533, 339}
                , new[] {143, 229}, new[] {326, -702}, new[] {432, 11}, new[] {118, 636}, new[] {-756, -461}
                , new[] {287, 168}, new[] {-724, 499}, new[] {694, 210}, new[] {-933, 796}, new[] {-533, -302}
                , new[] {-582, 984}, new[] {865, -104}, new[] {399, -380}, new[] {-554, -79}, new[] {4, 502}
                , new[] {211, 389}, new[] {942, -999}, new[] {518, 81}, new[] {-477, -991}, new[] {-83, -820}
                , new[] {-849, 132}, new[] {-873, 346}, new[] {-388, -702}, new[] {-96, -673}, new[] {943, -216}
                , new[] {207, 181}, new[] {-730, -499}, new[] {-906, 478}, new[] {-55, -966}, new[] {-679, 711}
                , new[] {-840, 0}, new[] {901, -323}, new[] {-289, -517}, new[] {198, 162}, new[] {334, 802}
                , new[] {-957, 1000}, new[] {-604, 392}, new[] {544, 199}, new[] {-444, 767}, new[] {-332, -229}
                , new[] {-35, -428}, new[] {-875, 938}, new[] {-702, -942}, new[] {-482, 780}, new[] {781, -470}
                , new[] {766, -127}, new[] {-206, -679}, new[] {-463, 948}, new[] {-885, -430}, new[] {-408, 153}
                , new[] {-516, 672}, new[] {-591, -311}, new[] {565, 79}, new[] {-346, 84}, new[] {591, 652}
                , new[] {627, -50}, new[] {-139, -709}, new[] {-37, 18}, new[] {228, -302}, new[] {910, 656}
                , new[] {-483, -61}, new[] {-165, 867}, new[] {259, -516}, new[] {-819, -149}, new[] {373, -10}
                , new[] {-906, -595}, new[] {-359, 610}, new[] {669, -994}, new[] {829, 446}, new[] {597, 737}
                , new[] {-903, 191}, new[] {947, 66}, new[] {573, 670}, new[] {706, 243}, new[] {-910, -83}
                , new[] {908, -306}, new[] {-219, -729}, new[] {-189, 193}, new[] {-906, -775}, new[] {-134, 709}
                , new[] {45, 150}, new[] {924, -964}, new[] {-737, -110}, new[] {930, -526}, new[] {788, -441}
                , new[] {-526, -169}, new[] {483, 426}, new[] {664, -143}, new[] {-620, -422}, new[] {-417, 297}
                , new[] {450, 167}, new[] {906, 151}, new[] {787, -498}, new[] {-557, -57}, new[] {-744, 843}
                , new[] {-599, 711}, new[] {370, -483}, new[] {672, -827}, new[] {380, 555}, new[] {-756, 949}
                , new[] {-768, 151}, new[] {-608, 478}, new[] {993, 146}, new[] {-288, -596}, new[] {-440, -333}
                , new[] {301, 42}, new[] {-428, -182}, new[] {-652, 954}, new[] {705, -330}, new[] {626, -706}
                , new[] {-405, 334}, new[] {657, -532}, new[] {528, -877}, new[] {-736, 101}, new[] {-751, -789}
                , new[] {23, -122}, new[] {995, -602}, new[] {-938, -57}, new[] {521, -731}, new[] {-432, -432}
                , new[] {-11, -376}, new[] {-190, 535}, new[] {-549, 889}, new[] {-570, 25}, new[] {-588, 477}
                , new[] {442, 668}, new[] {-575, -869}, new[] {-550, -9}, new[] {-239, -858}, new[] {705, -382}
                , new[] {-309, 447}, new[] {716, 652}, new[] {-782, 300}, new[] {803, -287}, new[] {-351, 806}
                , new[] {308, 908}, new[] {-640, 555}, new[] {-715, -356}, new[] {860, 310}, new[] {167, -708}
                , new[] {756, 266}, new[] {39, 297}, new[] {534, -871}, new[] {-522, 785}, new[] {798, -4}
                , new[] {767, 291}, new[] {135, -385}, new[] {-315, -288}, new[] {-806, 765}, new[] {96, 179}
                , new[] {562, 511}, new[] {344, 537}, new[] {-10, -650}, new[] {-241, -689}, new[] {-769, -758}
                , new[] {-704, 775}, new[] {752, -901}, new[] {797, -838}, new[] {236, 953}, new[] {-198, 563}
                , new[] {-382, -558}, new[] {-851, 397}, new[] {138, 954}, new[] {-185, 0}, new[] {-221, -964}
                , new[] {-861, -312}, new[] {425, -925}, new[] {-603, -202}, new[] {418, 965}, new[] {0, -186}
                , new[] {266, -922}, new[] {801, 935}, new[] {351, 461}, new[] {-861, -467}, new[] {-850, -684}
                , new[] {466, 709}, new[] {-931, 395}, new[] {443, -342}, new[] {-79, 783}, new[] {191, 806}
                , new[] {549, -388}, new[] {-780, -898}, new[] {-493, 194}, new[] {297, -364}, new[] {143, -162}
                , new[] {831, -468}, new[] {608, -921}, new[] {703, -262}, new[] {-36, 715}, new[] {233, 718}
                , new[] {-398, 957}, new[] {-221, 777}, new[] {189, 220}, new[] {687, 840}, new[] {-301, -433}
                , new[] {-776, -459}, new[] {-763, 290}, new[] {197, -151}, new[] {-137, 120}, new[] {-50, -914}
                , new[] {570, -902}, new[] {-96, -551}, new[] {739, 165}, new[] {579, -720}, new[] {-113, 952}
                , new[] {-619, -374}, new[] {244, -38}, new[] {6, -301}, new[] {-890, 891}, new[] {194, 451}
                , new[] {-770, -791}, new[] {-690, -941}, new[] {-207, -792}, new[] {-483, 406}, new[] {508, 145}
                , new[] {898, 873}, new[] {142, -83}, new[] {-380, -494}, new[] {904, -907}, new[] {630, -486}
                , new[] {-687, -720}, new[] {-919, -123}, new[] {364, 315}, new[] {668, 841}, new[] {29, -54}
                , new[] {-957, 841}, new[] {-465, -364}, new[] {-406, -407}, new[] {-827, 301}, new[] {-37, -40}
                , new[] {-550, 94}, new[] {-469, 496}, new[] {990, -927}, new[] {-999, 169}, new[] {965, -637}
                , new[] {361, -718}, new[] {11, -995}, new[] {-404, -834}, new[] {803, 27}, new[] {-867, -514}
                , new[] {576, 923}, new[] {703, 33}, new[] {277, 396}, new[] {-923, -532}, new[] {544, 85}
                , new[] {-821, 895}, new[] {366, 600}, new[] {-324, -282}, new[] {392, -208}, new[] {-118, 785}
                , new[] {-285, 94}, new[] {37, -63}, new[] {750, -86}, new[] {310, 432}, new[] {-261, -349}
                , new[] {244, 887}, new[] {-370, -549}, new[] {328, -153}, new[] {-173, -78}, new[] {-28, -887}
                , new[] {-845, 49}, new[] {254, -821}, new[] {342, 586}, new[] {215, 108}, new[] {721, 346}
                , new[] {-400, -504}, new[] {-250, -847}, new[] {-165, 731}, new[] {-528, -452}, new[] {-742, 318}
                , new[] {909, -665}, new[] {-896, 189}, new[] {-383, -955}, new[] {518, 79}, new[] {-46, 200}
                , new[] {391, -380}, new[] {895, 891}, new[] {-144, 689}, new[] {626, 842}, new[] {-707, 369}
                , new[] {-163, -68}, new[] {-522, 75}, new[] {602, 838}, new[] {154, 30}, new[] {5, 418}
                , new[] {-267, -581}, new[] {-874, 568}, new[] {266, -234}, new[] {-7, 149}, new[] {-878, -238}
                , new[] {-696, -585}, new[] {205, -739}, new[] {573, -423}, new[] {-420, -380}, new[] {-628, 628}
                , new[] {30, 621}, new[] {165, 900}, new[] {-723, 17}, new[] {680, -749}, new[] {-400, -985}
                , new[] {990, 598}, new[] {-629, -176}, new[] {-735, 195}, new[] {-902, -356}, new[] {168, 284}
                , new[] {-407, 847}, new[] {-433, -700}, new[] {-195, -894}, new[] {841, 888}, new[] {-737, 638}
                , new[] {-119, 497}, new[] {47, 649}, new[] {-206, 178}, new[] {-62, -121}, new[] {-874, -903}
                , new[] {-653, -21}, new[] {62, 23}, new[] {-116, 676}, new[] {947, -75}, new[] {663, 599}
                , new[] {406, -266}, new[] {40, -613}, new[] {397, -510}, new[] {847, -145}, new[] {798, -545}
                , new[] {-723, -657}, new[] {531, -943}, new[] {-196, -782}, new[] {-776, -886}, new[] {-902, 748}
                , new[] {501, -585}, new[] {448, 190}, new[] {252, 237}, new[] {453, 102}, new[] {-813, -846}
                , new[] {-715, -484}, new[] {391, 637}, new[] {-341, -451}, new[] {-468, -983}, new[] {-916, 608}
                , new[] {511, 296}, new[] {394, -701}, new[] {589, 786}, new[] {619, 888}, new[] {-64, -639}
                , new[] {329, 548}, new[] {477, 682}, new[] {-445, 757}, new[] {61, 857}, new[] {-358, -133}
                , new[] {-264, -511}, new[] {871, -159}, new[] {606, -729}, new[] {41, 999}, new[] {-549, 201}
                , new[] {120, 680}, new[] {-86, -644}, new[] {-383, -982}, new[] {-878, -110}, new[] {237, -140}
                , new[] {-697, 512}, new[] {151, 961}, new[] {119, -444}, new[] {-560, 748}, new[] {991, 361}
                , new[] {-438, -544}, new[] {657, -182}, new[] {353, -877}, new[] {245, -362}, new[] {140, -958}
                , new[] {-348, 633}, new[] {-921, 731}, new[] {-241, 333}, new[] {909, 241}, new[] {308, -207}
                , new[] {-371, -615}, new[] {831, 78}, new[] {-313, -92}, new[] {-745, -328}, new[] {272, -493}
                , new[] {244, 49}, new[] {-717, -961}, new[] {676, 776}, new[] {-618, -513}, new[] {338, -596}
                , new[] {-276, 117}, new[] {-116, 207}, new[] {-817, -407}, new[] {-721, -81}, new[] {851, 741}
                , new[] {580, -771}, new[] {-415, -199}, new[] {-760, -363}, new[] {-403, 733}, new[] {814, -483}
                , new[] {412, -287}, new[] {339, 251}, new[] {435, 216}, new[] {-177, 398}, new[] {-948, -930}
                , new[] {-691, -95}, new[] {-796, -790}, new[] {764, -959}, new[] {-82, 168}, new[] {-574, -21}
                , new[] {152, -642}, new[] {-154, -804}, new[] {-511, 567}, new[] {-964, 260}, new[] {21, -206}
                , new[] {-142, -400}, new[] {-196, 606}, new[] {894, 211}, new[] {820, 484}, new[] {34, 77}
                , new[] {-671, 664}, new[] {-653, -807}, new[] {-646, 850}, new[] {276, -447}, new[] {343, -716}
                , new[] {802, -717}, new[] {408, -150}, new[] {-926, -56}, new[] {140, 37}, new[] {-528, 694}
                , new[] {-392, -373}, new[] {266, -338}, new[] {34, 142}, new[] {-467, 65}, new[] {-407, -856}
                , new[] {526, 805}, new[] {579, -440}, new[] {-971, -975}, new[] {5, -757}, new[] {568, -486}
                , new[] {-475, -113}, new[] {-491, -394}, new[] {-474, -549}, new[] {-360, 72}, new[] {680, 554}
                , new[] {508, -412}, new[] {906, -987}, new[] {-428, -655}, new[] {274, -543}, new[] {-790, 263}
                , new[] {739, 244}, new[] {62, 702}, new[] {-772, 878}, new[] {-769, -991}, new[] {912, 309}
                , new[] {-658, 961}, new[] {397, -638}, new[] {-75, 90}, new[] {-551, 262}, new[] {887, -940}
                , new[] {707, 766}, new[] {-955, -270}, new[] {908, 267}, new[] {-923, -660}, new[] {-541, 352}
                , new[] {-849, 343}, new[] {648, -619}, new[] {141, -389}, new[] {122, 95}, new[] {373, 588}
                , new[] {273, 598}, new[] {-85, 550}, new[] {684, -543}, new[] {-531, 869}, new[] {413, 412}
                , new[] {247, 981}, new[] {773, 673}, new[] {-657, 663}, new[] {423, -735}, new[] {-439, 467}
                , new[] {-368, -419}, new[] {308, -725}, new[] {-553, -588}, new[] {431, -420}, new[] {218, -677}
                , new[] {649, 696}, new[] {969, -769}, new[] {558, 548}, new[] {-486, -28}, new[] {550, -647}
                , new[] {-282, -35}, new[] {395, 711}, new[] {189, 296}, new[] {488, 273}, new[] {540, 854}
                , new[] {780, -754}, new[] {-931, 833}, new[] {-719, -862}, new[] {-88, -487}, new[] {591, -165}
                , new[] {584, -640}, new[] {502, -308}, new[] {219, 390}, new[] {713, -732}, new[] {228, -759}
                , new[] {488, 675}, new[] {-300, -153}, new[] {105, 269}, new[] {-240, 462}, new[] {-825, -638}
                , new[] {299, -65}, new[] {-576, -222}, new[] {-929, 481}, new[] {-404, 687}, new[] {-574, -81}
                , new[] {-365, -844}, new[] {530, 298}, new[] {-681, 676}, new[] {-304, -528}, new[] {-4, 431}
                , new[] {-35, 311}, new[] {669, -392}, new[] {-998, 983}, new[] {466, -350}, new[] {-62, -457}
                , new[] {-807, 7}, new[] {821, -835}, new[] {-291, -433}, new[] {171, -510}, new[] {-309, -509}
                , new[] {-981, 422}, new[] {-820, -753}, new[] {342, 803}, new[] {79, 886}, new[] {-233, 551}
                , new[] {-595, -272}, new[] {289, -159}, new[] {470, 36}, new[] {-882, -773}, new[] {916, 446}
                , new[] {-79, -500}, new[] {-240, 199}, new[] {703, 391}, new[] {-316, -273}, new[] {-659, -781}
                , new[] {836, -409}, new[] {-688, -287}, new[] {230, -254}, new[] {575, -529}, new[] {-431, -985}
                , new[] {-451, 244}, new[] {-503, -321}, new[] {-196, -116}, new[] {-340, -460}, new[] {433, -428}
                , new[] {-977, 127}, new[] {-256, 485}, new[] {-940, 876}, new[] {574, 646}, new[] {-596, -315}
                , new[] {976, 249}, new[] {108, -983}, new[] {277, 388}, new[] {269, 6}, new[] {504, 734}
                , new[] {-267, 86}, new[] {143, -979}, new[] {504, -569}, new[] {827, -935}, new[] {-499, -63}
                , new[] {-67, 117}, new[] {472, -31}, new[] {-904, -361}, new[] {-122, 700}, new[] {-889, 978}
                , new[] {620, -954}, new[] {-396, 248}, new[] {-885, -588}, new[] {1000, 438}, new[] {305, -635}
                , new[] {455, 620}, new[] {-566, 445}, new[] {-107, 114}, new[] {-317, 575}, new[] {-241, 58}
                , new[] {717, -52}, new[] {977, 900}, new[] {-603, -659}, new[] {-266, 692}, new[] {-750, -769}
                , new[] {952, -648}, new[] {-213, -545}, new[] {715, 129}, new[] {-160, 126}, new[] {322, -162}
                , new[] {205, 386}, new[] {608, 654}, new[] {-50, -281}, new[] {804, -274}, new[] {303, 916}
                , new[] {326, -394}, new[] {174, -167}, new[] {-599, 215}, new[] {-927, -248}, new[] {631, -973}
                , new[] {-335, -946}, new[] {-1000, -83}, new[] {108, 292}, new[] {773, 895}, new[] {-674, 183}
                , new[] {471, 863}, new[] {-363, -235}, new[] {-271, 575}, new[] {-199, 178}, new[] {-170, 805}
                , new[] {-443, -327}, new[] {659, -734}, new[] {132, -63}, new[] {161, -880}, new[] {110, 55}
                , new[] {-969, 981}, new[] {780, 153}, new[] {-363, -552}, new[] {20, 110}, new[] {-850, 845}
                , new[] {-434, -621}, new[] {724, 553}, new[] {-251, 435}, new[] {-964, -915}, new[] {983, 280}
                , new[] {284, 191}, new[] {850, 999}, new[] {-951, -893}, new[] {145, -971}, new[] {186, -382}
                , new[] {-960, 917}, new[] {586, -120}, new[] {-422, -881}, new[] {900, 459}, new[] {215, -279}
                , new[] {-186, -870}, new[] {460, 277}, new[] {-703, 369}
            };
            var K = 499;

            // Act
            var result = new Solution().KClosest(points, K);

            // Assert
            CollectionAssert.AreEquivalent(new[]
            {
                new[] {-37, 18}, new[] {10, -50}, new[] {-37, -40}, new[] {18, -58}, new[] {29, -54}, new[] {62, 23}
                , new[] {37, -63}, new[] {31, -77}, new[] {34, 77}, new[] {105, -21}, new[] {20, 110}, new[] {-75, 90}
                , new[] {110, 55}, new[] {23, -122}, new[] {-67, 117}, new[] {-62, -121}, new[] {140, 37}
                , new[] {34, 142}, new[] {132, -63}, new[] {-7, 149}, new[] {122, 95}, new[] {155, -6}
                , new[] {-107, 114}, new[] {45, 150}, new[] {154, 30}, new[] {142, -83}, new[] {-25, -164}
                , new[] {-163, -68}, new[] {-137, 120}, new[] {-185, 0}, new[] {0, -186}, new[] {-82, 168}
                , new[] {-173, -78}, new[] {-136, 140}, new[] {-109, 170}, new[] {96, 179}, new[] {-160, 126}
                , new[] {-46, 200}, new[] {21, -206}, new[] {143, -162}, new[] {-134, 172}, new[] {158, 161}
                , new[] {-196, -116}, new[] {-228, 21}, new[] {-116, 207}, new[] {215, 108}, new[] {174, -167}
                , new[] {-243, -10}, new[] {222, 101}, new[] {244, -38}, new[] {-241, 58}, new[] {197, -151}
                , new[] {-248, -18}, new[] {244, 49}, new[] {178, 182}, new[] {198, 162}, new[] {22, -256}
                , new[] {-19, -257}, new[] {-199, 178}, new[] {116, -241}, new[] {-238, -124}, new[] {269, 6}
                , new[] {99, 251}, new[] {143, 229}, new[] {-189, 193}, new[] {-206, 178}, new[] {50, 270}
                , new[] {207, 181}, new[] {237, -140}, new[] {-267, 86}, new[] {-282, -35}, new[] {-50, -281}
                , new[] {-214, 192}, new[] {105, 269}, new[] {189, 220}, new[] {-286, -76}, new[] {39, 297}
                , new[] {-276, 117}, new[] {-285, 94}, new[] {6, -301}, new[] {301, 42}, new[] {292, 88}
                , new[] {299, -65}, new[] {-185, 247}, new[] {108, 292}, new[] {-240, 199}, new[] {-35, 311}
                , new[] {316, -64}, new[] {-227, 231}, new[] {-313, -92}, new[] {87, -315}, new[] {289, -159}
                , new[] {168, 284}, new[] {287, 168}, new[] {-10, -333}, new[] {-110, -315}, new[] {236, -236}
                , new[] {325, 77}, new[] {-300, -153}, new[] {284, 191}, new[] {230, -254}, new[] {-208, 274}
                , new[] {252, 237}, new[] {-195, 286}, new[] {189, 296}, new[] {215, -279}, new[] {266, -234}
                , new[] {-27, -355}, new[] {-346, 84}, new[] {322, -162}, new[] {-326, 157}, new[] {328, -153}
                , new[] {-360, 72}, new[] {-329, -165}, new[] {308, -207}, new[] {55, 368}, new[] {373, -10}
                , new[] {-183, -327}, new[] {-11, -376}, new[] {372, 64}, new[] {228, -302}, new[] {-358, -133}
                , new[] {-177, -339}, new[] {-375, -84}, new[] {332, -194}, new[] {-13, 391}, new[] {390, 54}
                , new[] {-332, -229}, new[] {135, -385}, new[] {-348, 218}, new[] {-241, 333}, new[] {-366, -188}
                , new[] {141, -389}, new[] {-373, -181}, new[] {-316, -273}, new[] {5, 418}, new[] {-196, -370}
                , new[] {199, -371}, new[] {339, 251}, new[] {-143, -398}, new[] {-142, -400}, new[] {186, -382}
                , new[] {133, -404}, new[] {47, 424}, new[] {-315, -288}, new[] {-35, -428}, new[] {-324, -282}
                , new[] {344, -258}, new[] {266, -338}, new[] {-4, 431}, new[] {227, 367}, new[] {432, 11}
                , new[] {-98, -421}, new[] {-363, -235}, new[] {-407, 151}, new[] {408, -150}, new[] {-177, 398}
                , new[] {-408, 153}, new[] {-261, -349}, new[] {205, 386}, new[] {245, -362}, new[] {359, -253}
                , new[] {211, 389}, new[] {392, -208}, new[] {407, 180}, new[] {-229, 382}, new[] {445, 27}
                , new[] {219, 390}, new[] {-190, -405}, new[] {177, 415}, new[] {-337, -307}, new[] {119, -444}
                , new[] {453, 79}, new[] {-415, -199}, new[] {-62, -457}, new[] {453, 102}, new[] {267, -380}
                , new[] {-428, -182}, new[] {-396, 248}, new[] {297, -364}, new[] {-406, 237}, new[] {470, 36}
                , new[] {-467, 65}, new[] {-343, -325}, new[] {472, -31}, new[] {450, -150}, new[] {277, 388}
                , new[] {450, 167}, new[] {364, 315}, new[] {277, 396}, new[] {-3, -485}, new[] {406, -266}
                , new[] {435, 216}, new[] {448, 190}, new[] {-486, -28}, new[] {-483, -61}, new[] {-10, 488}
                , new[] {-475, -113}, new[] {194, 451}, new[] {-391, 302}, new[] {-88, -487}, new[] {484, -104}
                , new[] {311, 388}, new[] {-485, -117}, new[] {4, 502}, new[] {412, -287}, new[] {-251, 435}
                , new[] {-499, -63}, new[] {-79, -500}, new[] {-298, 411}, new[] {-119, 497}, new[] {453, 237}
                , new[] {326, -394}, new[] {-417, 297}, new[] {352, -372}, new[] {-451, 244}, new[] {-371, -354}
                , new[] {490, -161}, new[] {-240, 462}, new[] {-291, -433}, new[] {518, 79}, new[] {518, 81}
                , new[] {324, 413}, new[] {-405, 334}, new[] {276, -447}, new[] {411, -328}, new[] {517, 100}
                , new[] {369, -376}, new[] {-301, -433}, new[] {-522, 75}, new[] {-528, -12}, new[] {508, 145}
                , new[] {-493, 194}, new[] {-495, 190}, new[] {526, -77}, new[] {310, 432}, new[] {110, 523}
                , new[] {460, 277}, new[] {-411, -346}, new[] {171, -510}, new[] {94, -531}, new[] {-392, -373}
                , new[] {-340, -421}, new[] {-248, -483}, new[] {-309, 447}, new[] {-470, 273}, new[] {-437, -325}
                , new[] {391, -380}, new[] {-256, 485}, new[] {536, 122}, new[] {-550, -9}, new[] {544, 85}
                , new[] {-443, -327}, new[] {512, -203}, new[] {399, -380}, new[] {-440, -333}, new[] {-526, -169}
                , new[] {-183, 523}, new[] {-68, 550}, new[] {-537, -142}, new[] {-85, 550}, new[] {-368, -419}
                , new[] {-550, 94}, new[] {488, 273}, new[] {-96, -551}, new[] {-554, -79}, new[] {443, -342}
                , new[] {-557, -57}, new[] {316, 465}, new[] {-259, 499}, new[] {-234, -512}, new[] {272, -493}
                , new[] {-341, -451}, new[] {-420, -380}, new[] {-190, 535}, new[] {565, 79}, new[] {-570, 25}
                , new[] {-340, -460}, new[] {-417, -392}, new[] {563, -105}, new[] {534, -210}, new[] {-574, -21}
                , new[] {-406, -407}, new[] {-264, -511}, new[] {259, -516}, new[] {544, 199}, new[] {351, 461}
                , new[] {-574, -81}, new[] {-188, -551}, new[] {466, -350}, new[] {413, 412}, new[] {-549, 201}
                , new[] {-213, -545}, new[] {-580, -85}, new[] {502, -308}, new[] {502, 309}, new[] {-465, -364}
                , new[] {511, 296}, new[] {-289, -517}, new[] {-309, -509}, new[] {590, -85}, new[] {-503, -321}
                , new[] {-198, 563}, new[] {586, -120}, new[] {-233, 551}, new[] {28, 600}, new[] {431, -420}
                , new[] {530, 298}, new[] {274, -543}, new[] {370, -483}, new[] {433, -428}, new[] {-304, -528}
                , new[] {-551, 262}, new[] {-432, -432}, new[] {-533, -302}, new[] {-612, -30}, new[] {591, -165}
                , new[] {40, -613}, new[] {142, 599}, new[] {-576, -222}, new[] {30, 621}, new[] {-579, -227}
                , new[] {-380, -494}, new[] {535, -320}, new[] {109, 616}, new[] {627, -50}, new[] {-491, -394}
                , new[] {-483, 406}, new[] {-533, 339}, new[] {-319, 546}, new[] {-213, -596}, new[] {595, -222}
                , new[] {-271, 575}, new[] {-603, -202}, new[] {-599, 215}, new[] {-196, 606}, new[] {344, 537}
                , new[] {329, 548}, new[] {-267, -581}, new[] {-116, -629}, new[] {-439, 467}, new[] {-64, -639}
                , new[] {-400, -504}, new[] {483, 426}, new[] {-503, 403}, new[] {-541, 352}, new[] {397, -510}
                , new[] {118, 636}, new[] {-647, -7}, new[] {-601, -241}, new[] {-86, -644}, new[] {-10, -650}
                , new[] {47, 649}, new[] {-642, -110}, new[] {-629, -176}, new[] {-653, -21}, new[] {508, -412}
                , new[] {-595, -272}, new[] {-317, 575}, new[] {273, 598}, new[] {-611, -244}, new[] {152, -642}
                , new[] {-363, -552}, new[] {-288, -596}, new[] {-370, -549}, new[] {561, 355}, new[] {-165, 646}
                , new[] {-591, -311}, new[] {-384, -547}, new[] {611, 276}, new[] {402, 538}, new[] {549, -388}
                , new[] {380, 555}, new[] {-596, -315}, new[] {-486, 470}, new[] {-382, -558}, new[] {342, 586}
                , new[] {664, -143}, new[] {-96, -673}, new[] {641, -227}, new[] {287, 617}, new[] {584, -350}
                , new[] {657, -182}, new[] {-534, 424}, new[] {-469, 496}, new[] {-684, -25}, new[] {338, -596}
                , new[] {-116, 676}, new[] {547, -416}, new[] {688, 21}, new[] {120, 680}, new[] {666, -186}
                , new[] {315, 617}, new[] {650, -240}, new[] {161, -675}, new[] {478, -504}, new[] {-528, -452}
                , new[] {373, 588}, new[] {-682, -142}, new[] {-691, -95}, new[] {-674, 183}, new[] {-438, -544}
                , new[] {366, 600}, new[] {703, 33}, new[] {-144, 689}, new[] {305, -635}, new[] {62, 702}
                , new[] {691, -139}, new[] {696, -113}, new[] {593, -382}, new[] {-359, 610}, new[] {-206, -679}
                , new[] {389, 594}, new[] {-619, -348}, new[] {-122, 700}, new[] {-603, -376}, new[] {218, -677}
                , new[] {573, -423}, new[] {-369, -610}, new[] {703, 129}, new[] {696, 165}, new[] {-36, 715}
                , new[] {-371, -615}, new[] {717, -52}, new[] {-566, 445}, new[] {-604, 392}, new[] {-134, 709}
                , new[] {-692, 205}, new[] {-348, 633}, new[] {-139, -709}, new[] {-582, -429}, new[] {-723, 17}
                , new[] {-619, -374}, new[] {694, 210}, new[] {-474, -549}, new[] {-721, -81}, new[] {715, 129}
                , new[] {-668, 286}, new[] {579, -440}, new[] {167, -708}, new[] {-241, -689}, new[] {-615, -396}
                , new[] {-144, -722}, new[] {653, -344}, new[] {-409, 617}, new[] {-719, -178}, new[] {-591, 447}
                , new[] {-266, 692}, new[] {-736, 101}, new[] {-737, -110}, new[] {-688, -287}, new[] {706, 243}
                , new[] {-85, 742}, new[] {391, 637}, new[] {568, -486}, new[] {-165, 731}, new[] {-620, -422}
                , new[] {703, -262}, new[] {397, -638}, new[] {233, 718}, new[] {750, -86}, new[] {5, -757}
                , new[] {-588, 477}, new[] {739, 165}, new[] {-434, -621}, new[] {572, 497}, new[] {562, 511}
                , new[] {504, -569}, new[] {-735, 195}, new[] {-219, -729}, new[] {-352, 675}, new[] {-710, -275}
                , new[] {-511, 567}, new[] {64, 762}, new[] {205, -739}, new[] {155, -753}, new[] {455, 620}
                , new[] {501, -585}, new[] {-570, -519}, new[] {-608, 478}, new[] {-624, 457}, new[] {326, -702}
                , new[] {-673, 385}, new[] {669, -392}, new[] {242, 737}, new[] {766, -127}, new[] {-747, -212}
                , new[] {-774, -67}, new[] {739, 244}, new[] {705, -330}, new[] {-211, 752}, new[] {-757, 193}
                , new[] {575, -529}, new[] {-707, 334}, new[] {558, 548}, new[] {-777, 92}, new[] {-428, -655}
                , new[] {-768, 151}, new[] {-210, -755}
            }, result);
        }
    }
}