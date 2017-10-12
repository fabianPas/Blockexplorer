using System;
using System.Collections.Generic;
using System.Text;

namespace Blockexplorer.Core.Domain
{
    /// <summary>
    /// Model returns values from the 'getstakinginfo' RPC call.
    /// </summary>
    public class StakingInfo
    {
        public int CurrentBlockSize { get; set; }
        public decimal Difficulty { get; set; }
        public int SearchInterval { get; set; }
        public long NetStakeWeight { get; set; }
    }
}
