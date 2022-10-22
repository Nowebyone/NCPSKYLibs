using NCPLib.BaseLib;

namespace NCPLib
{
    public class MathFTools : BaseManager<MathFTools>
    {
        /// <summary>
        /// 吸附数值
        /// </summary>
        /// <param name="num"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static float CaliberNum(float num, float step)
        {
            bool tmp_g0 = num > 0;
            int flag = tmp_g0 ? 1 : -1;
            num = tmp_g0 ? num : -num;
            
            var tmp_k = (int)(num / step);
            var tmp_l = tmp_k * step;
            var tmp_h = (tmp_k + 1) * step;
            var tmp_hd = tmp_h - num;
            var tmp_ld = num - tmp_l;
            return flag * (tmp_hd >= tmp_ld ? tmp_l : tmp_h);
        }




    }
}
