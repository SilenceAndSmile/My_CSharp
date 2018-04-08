	/*
	 * Created by SharpDevelop.
	 * User: LiJun
	 * Date: 2018/4/8
	 * Time: 1:05
	 * 
	 * To change this template use Tools | Options | Coding | Edit Standard Headers.
	 */
	
	using System;
	
	namespace LJGoldbach
	{
		
		class Program
		{
			public static long RANGE = 1000000L;  // 待验证偶数范围
			
			public static void Main(string[] args)
			{
				DateTime beforDT = System.DateTime.Now;  // 程序计时
				
				// step1: 先找出这个范围内所有的质数
				// 预定义数个变量来方便后续算法运行和解释
				var Num = 0;  // 质数个数计数器
				var Prime = new long[RANGE];  // 质数存储数组。
				var CompositeJudge = new bool[RANGE];  // 合数判别数组
				CompositeJudge[0] = CompositeJudge[1] = true;  //0和1显然不是质数，故他们的合数判别数组对应值为true。
				
				// 利用欧拉改进的埃拉托斯特尼筛法快速筛选指定范围内的质数
				for (long i = 2; i < RANGE; ++i) {
					if (!CompositeJudge[i])
						Prime[Num++] = i;
					
					for (long j = 0; j < Num && i * Prime[j] < RANGE; ++j) {
						CompositeJudge[i * Prime[j]] = true;
						if ((i % Prime[j]) == 0L)
							break;
					}
				}
				
				// step2: 遍历法查找给定范围内的偶数是否可以被分解为两个质数之和
				long Even = 4L;  // 待判定偶数
				while (Even <= RANGE) {
					bool Flag = false;	//设置Flag是为了让算法找到对某个偶数一种可能的分解就停止，从而加快判别效率。
				
					for (int m = 0; m < Num && m < Even; ++m) { //循环在验证时的质数必须存在且小于给定的偶数
						// 如果这个偶数减去当前的质数得到的数仍然为质数，则意味着我们找到了一种分解
						if (CompositeJudge[Even - Prime[m]] == false) {
							Flag = true;
							// Console.WriteLine("偶数{0} = {1} + {2}", Even, Prime[m], Even - Prime[m]);
							break;
						}
					}
									
					if (Flag == false) { 
						// 如果对某个偶数验证完所有比他小的质数时都不存在一种可能的分解(Flag = false);
						// 此时输出这个独特的偶数并终止算法(强哥德巴赫猜想将不成立)。
						Console.WriteLine("强哥德巴赫猜想对于数{0}不成立！", Even);
						break;
					} else {
						// 对某个偶数存在一种可能的分解时,重置判别符并开始准备验证下一个偶数
						Flag = false;
						Even = Even + 2;
					}			
				}
				
				
				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforDT);
				Console.WriteLine("在不大于{0}的范围内筛选所有质数并验证强哥德巴赫猜想共花费{1}ms.", RANGE, ts.TotalMilliseconds);			
				Console.ReadKey(true);
			}
		}
	}
