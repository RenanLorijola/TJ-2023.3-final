using UnityEngine;

namespace Ursaanimation.CubicFarmAnimals
{
    public class AnimationController : MonoBehaviour
    {
        public Animator animator;
        public string walkForwardAnimation = "walk_forward";
        public string walkBackwardAnimation = "walk_backwards";
        public string runForwardAnimation = "run_forward";
        public string turn90LAnimation = "turn_90_L";
        public string turn90RAnimation = "turn_90_R";
        public string trotAnimation = "trot_forward";
        public string sittostandAnimation = "sit_to_stand";
        public string standtositAnimation = "stand_to_sit";
        public float walkSpeed = 20; // Velocidade de caminhada
        public float runSpeed = 40; // Velocidade de corrida
        Vector3 pos ; // Direção aleatória para caminhada
        Vector3 localVectorForward;
        Vector3 localVectorBackward;
        float tempo;

        void Start()
        {
            pos = transform.position;
            tempo = 0;
            animator = GetComponent<Animator>();
            SetRandomWalkAnimation(); // Inicia com a animação
        }

        void Update()
        {
            localVectorForward = transform.TransformDirection(5,0,0);
            localVectorBackward = transform.TransformDirection(5,0,0);
            tempo+= Time.deltaTime;
            if( tempo > 10){
                SetRandomWalkAnimation();
            }
        }


         // Define uma animação aleatória de caminhada
        private void SetRandomWalkAnimation()
        {
            int randomAnimation = Random.Range(0, 7); 
            //Escolhe uma animação aleatória
            switch (randomAnimation) 
            {
                case 0:
                    animator.Play(walkForwardAnimation);
                    transform.Translate(localVectorForward * walkSpeed * Time.deltaTime);
                    break;
                case 1:
                    animator.Play(walkBackwardAnimation);
                     transform.Translate(localVectorBackward * walkSpeed * Time.deltaTime);
                    break;
                case 2:
                    animator.Play(runForwardAnimation);
                     transform.Translate(localVectorForward * runSpeed * Time.deltaTime);
                    break;
                case 3:
                    animator.Play(turn90LAnimation);
                    break;
                case 4:
                    animator.Play(turn90RAnimation);
                    break;
                case 5:
                    animator.Play(trotAnimation);
                    transform.Translate(localVectorForward * walkSpeed * Time.deltaTime);
                    break;
                case 6:
                    animator.Play(sittostandAnimation);
                    break;
                case 7:
                    animator.Play(standtositAnimation);
                    break;
            }
            tempo = 0;
        }
    }
}
