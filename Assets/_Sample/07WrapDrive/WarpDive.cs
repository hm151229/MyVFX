using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

namespace MyVfx
{
    /// <summary>
    /// WarpDrive 이펙트 효과
    /// </summary>
    public class WarpDive : MonoBehaviour
    {
        #region Variables
        public VisualEffect warpVfx;
        public MeshRenderer warpMeshRenderer;

        [SerializeField]
        private float rate = 0.1f;
        //파라미터
        private string WarpAmount = "WarpAmount";
        private string Active = "_Active";

        #endregion

        #region Unity Event Method
        private void Start()
        {
            //이펙트 효과 초기화 (멈춤)
            warpVfx.Stop();
            warpVfx.SetFloat(WarpAmount, 0f);
            warpMeshRenderer.material.SetFloat(Active, 0f);
        }

        private void Update()
        {
            //이펙트 효과 on
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ActiveParticle(true));
                StartCoroutine(ActiveShader(true));
            }
            //이펙트 효과 off
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine(ActiveParticle(false));
                StartCoroutine(ActiveShader(false));
            }
        }
        #endregion

        #region Custom Method
        //warpVfx ON/OFF
        IEnumerator ActiveParticle(bool active)
        {
            if(active)
            {
                warpVfx.Play();
                //0 -> 1
                float amount = warpVfx.GetFloat(WarpAmount);
                while (amount < 1f)
                {
                    amount += rate;
                    warpVfx.SetFloat(WarpAmount, amount);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                //1 -> 0
                float amount = warpVfx.GetFloat(WarpAmount);
                while (amount > 0f)
                {
                    amount -= rate;
                    warpVfx.SetFloat(WarpAmount, amount);
                    yield return new WaitForSeconds(0.1f);

                    if(amount < rate)
                    {
                        amount = 0f;    
                        warpVfx.SetFloat(WarpAmount, amount);
                        warpVfx.Stop();
                    }
                }
            }
        }

        IEnumerator ActiveShader(bool  active)
        {
            if (active)
            {
                //0 -> 1
                float amount = warpMeshRenderer.material.GetFloat(Active);
                warpMeshRenderer.material.SetFloat(Active, 0f);

                while (amount < 1f)
                {
                    amount += rate;
                    warpMeshRenderer.material.SetFloat(Active, amount);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                //1 -> 0
                float amount = warpMeshRenderer.material.GetFloat(Active);
                while (amount > 0f)
                {
                    amount -= rate;
                    warpMeshRenderer.material.SetFloat(Active, amount);
                    yield return new WaitForSeconds(0.1f);

                    if (amount < rate)
                    {
                        amount = 0f;
                        warpMeshRenderer.material.SetFloat(Active, amount);
                    }
                }
            }
        }
        #endregion
    }
}
