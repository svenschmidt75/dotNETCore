#region Copyright Quest Integrity Group, LLC 2018

// www.QuestIntegrity.com
// +1-303-415-1475
//  
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// 
// File Created: 2018-11-26 6:21 AM
// Updated:      2018-11-26 7:34 AM
// Created by:   Schmidt, Sven

#endregion

using Serilog;
using Xunit;

namespace Matching.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var source = new[]
            {
                new Match {Odom = 10, JL = 1}, new Match {Odom = 20, JL = 2}, new Match {Odom = 30, JL = 4}
            };

            var target = new[]
            {
                new Match {Odom = 10, JL = 1}, new Match {Odom = 20, JL = 2}, new Match {Odom = 30, JL = 4}
            };
            var matching = new Matching();

            // Act
            matching.Match(source, target, 0, 1, Log.Information);

            // Assert
        }

        [Fact]
        public void Test2()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("log.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Arrange
            var y1Rows = new[]
            {
                new Match {Odom = 0, JL = 0}                //  0
                , new Match {Odom = 1.85, JL = 2}           //  1
                , new Match {Odom = 2.292, JL = 2.992}      //  2
                , new Match {Odom = 5.283, JL = 8.542}      //  3
                , new Match {Odom = 13.825, JL = 2.783}     //  4
                , new Match {Odom = 16.608, JL = 38.583}    //  5
                , new Match {Odom = 55.192, JL = 29.567}    //  6
                , new Match {Odom = 84.758, JL = 2.925}     //  7
                , new Match {Odom = 87.683, JL = 2.925}     //  8
                , new Match {Odom = 92.475, JL = 4.792}     //  9
                , new Match {Odom = 102.467, JL = 9.992}    // 10
                , new Match {Odom = 105.442, JL = 2.975}    // 11
                , new Match {Odom = 110.108, JL = 4.667}    // 12
                , new Match {Odom = 129.450, JL = 19.342}   // 13
                , new Match {Odom = 133.233, JL = 3.783}    // 14
                , new Match {Odom = 138.033, JL = 4.800}    // 15
                , new Match {Odom = 182.367, JL = 44.333}   // 16
                , new Match {Odom = 202.350, JL = 19.983}   // 17
            };

            var y2Rows = new[]
            {
                new Match {Odom = -55.88, JL = 0}           //  0
                , new Match {Odom = -1.92, JL = 3.76}       //  1
                , new Match {Odom = 1.85, JL = 12.79}       //  2
                , new Match {Odom = 52.44, JL = 29.56}      //  3
                , new Match {Odom = 82.01, JL = 3.18}       //  4
                , new Match {Odom = 85.19, JL = 4.56}       //  5
                , new Match {Odom = 89.75, JL = 10.34}      //  6
                , new Match {Odom = 100.09, JL = 3.05}      //  7
                , new Match {Odom = 103.14, JL = 4.7}       //  8
                , new Match {Odom = 107.83, JL = 19.01}     //  9
                , new Match {Odom = 126.84, JL = 3.93}      // 10
                , new Match {Odom = 130.78, JL = 4.69}      // 11
                , new Match {Odom = 135.46, JL = 44.28}     // 12
                , new Match {Odom = 179.74, JL = 20.02}     // 13
                , new Match {Odom = 199.76, JL = 5.41}      // 14
                , new Match {Odom = 205.18, JL = 4.81}      // 15
                , new Match {Odom = 209.98, JL = 40.24}     // 16
            };
            var matching = new Matching();

            // Act
            matching.Match(y2Rows, y1Rows, 0, 10, Log.Information);

            // Assert
        }
    }
}
