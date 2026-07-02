namespace holy_Outlanguage_impl
{
    internal class Impl
    {
        string[] reservedWord = { "This", "is", "an", "Out", "R", "a", "g", "e", "Against", "Luminara", "." }; //예약어

        static List<token> code = new List<token>();

        public enum token
        {
            input, //입력
            output, //출력
            substitute, // 대입
            ASCIIlize, //아스키 코드화
            variableName, //변수명
            Comment, //주석
            Loop, // 반복문
            Conditional, //조건문
            Operator, // 연산자
            Bug, // 문법 오류
        }

        static void Main()
        {
            Console.WriteLine("Outlanguage 1.0.0v");

            string Icode = Console.ReadLine();

            tokenExtraction(Icode);

            foreach (var i in code)
            {
                Console.WriteLine(i);
            }

        }

        static void tokenExtraction(string codeline)
        {

            bool isStart = false;
            bool tokenEnd = false;
            List<Char> Token = new List<char>();

            for (int i = 0; i < codeline.Length; i++)
            {

                

                // 토큰간 구분
                if (char.IsWhiteSpace(codeline[i]))
                {
                    if (isStart)
                    {
                        //Console.WriteLine($"{i}번째 자리에서 띄워쓰기를 찾았습니다!");

                        //토글
                        if (!tokenEnd)
                        {
                            tokenEnd = true;

                            string token_ = new string(Token.ToArray());

                            token tokenKind = tokenGenerator(token_);

                            code.Add(tokenKind);

                            Token.Clear();
                        }

                    }

                }
                //글자
                else
                {
                    //첫번째 공백인지
                    if (!isStart)
                        isStart = true;

                    tokenEnd = false;
                }


                if (!tokenEnd)
                    Token.Add(codeline[i]);
                
            }

            if (Token.Count > 0)
            {
                string token_ = new string(Token.ToArray());
                token tokenKind = tokenGenerator(token_);
                code.Add(tokenKind);
            }

        }

        public static token tokenGenerator(string input)
        {
            // switch 식의 결과를 바로 return 합니다.
            return input switch
            {
                "This" => token.input,
                "Out" => token.output,
                "is" => token.substitute,
                "an" => token.ASCIIlize,
                "R" or "a" or "g" or "e" => token.variableName,
                "Against" => token.Conditional,
                "Luminara" => token.Loop,
                "." => token.Comment,

                _ => token.Bug,
            };
        }

    }
}
