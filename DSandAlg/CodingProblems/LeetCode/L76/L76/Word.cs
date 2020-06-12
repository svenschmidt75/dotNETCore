#region

using System.Collections.Generic;

#endregion

namespace L76
{
    public class Word
    {
        private readonly IDictionary<char, int> _map = new Dictionary<char, int>();
        private readonly IDictionary<char, int> _reference = new Dictionary<char, int>();

        public Word(string referenceWord)
        {
            for (var i = 0; i < referenceWord.Length; i++)
            {
                var c = referenceWord[i];
                if (_reference.TryGetValue(c, out var freq))
                {
                    _reference[c] += 1;
                }
                else
                {
                    _reference[c] = 1;
                }
            }
        }

        public bool IsComplete
        {
            get
            {
                // SS: true, if all chars from reference are contained
                foreach (var refItem in _reference)
                {
                    var (c, refFreq) = refItem;
                    if (_map.TryGetValue(c, out var freq))
                    {
                        if (freq < refFreq)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public void RemoveChar(char c)
        {
            if (_map.TryGetValue(c, out var freq))
            {
                if (freq == 1)
                {
                    _map.Remove(c);
                }
                else
                {
                    _map[c] -= 1;
                }
            }
        }

        public void AddChar(char c)
        {
            if (IsValidChar(c) == false)
            {
                return;
            }

            if (_map.TryGetValue(c, out var freq))
            {
                _map[c] += 1;
            }
            else
            {
                _map[c] = 1;
            }
        }

        public bool IsValidChar(char c)
        {
            return _reference.ContainsKey(c);
        }
    }
}