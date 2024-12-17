namespace Day17
{
    public class Cpu
    {
        private long _regA;
        private long _regB;
        private long _regC;
        private readonly int[] _operations;
        private int _index;
        
        private readonly List<int> _output;
        public IEnumerable<int> Output => _output;
        
        public Cpu(long regA, long regB, long regC, int[] operations)
        {
            _regA = regA;
            _regB = regB;
            _regC = regC;
            _operations = operations;
            _output = new List<int>();
        }

        public void RunProgram()
        {
            _index = 0;
            var goNext = true;

            while (goNext)
            {
                var instraction = _operations[_index];
                var operand = _operations[_index + 1];
                var isJump = false;
                switch (instraction)
                {
                    case 0:
                        Adv(operand);
                        break;
                    case 1:
                        Bxl(operand);
                        break;
                    case 2:
                        Bst(operand);
                        break;
                    case 3:
                        isJump = Jnz(operand);
                        break;
                    case 4:
                        Bxc();
                        break;
                    case 5:
                        Out(operand);
                        break;
                    case 6:
                        Bdv(operand);
                        break;
                    case 7:
                        Cdv(operand);
                        break;
                }

                if (isJump)
                {
                    continue;
                }

                if (_index >= _operations.Length - 2)
                {
                    goNext = false;
                }
    
                _index += 2;
            }

        }
        
        void Adv(long operand)
        {
            operand = GetComboOperand(operand);
            var denominator = Math.Pow(2, operand);
            _regA = (long)(_regA / denominator);
        }
        
        void Bxl(int operand)
        {
            _regB ^= operand;
        }
        
        void Bst(long operand)
        {
            operand = GetComboOperand(operand);
            _regB = operand % 8;
        }
        
        bool Jnz(int operand)
        {
            if (_regA == 0)
                return false;
            _index = operand;
            return true;
        }
        
        void Bxc()
        {
            _regB ^= _regC;
        }
        
        void Out(long operand)
        {
            operand = GetComboOperand(operand);
            var result = operand % 8;
            _output.Add((int)result);
        }
        
        void Bdv(long operand)
        {
            operand = GetComboOperand(operand);
            var denominator = Math.Pow(2, operand);
            _regB = (long)(_regA / denominator);
        }
        
        void Cdv(long operand)
        {
            operand = GetComboOperand(operand);
            var denominator = Math.Pow(2, operand);
            _regC = (long)(_regA / denominator);
        }
        
        long GetComboOperand(long operand) => operand switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => _regA,
            5 => _regB,
            6 => _regC,
            _ => throw new Exception("Invalid operand")
        };
    }
}