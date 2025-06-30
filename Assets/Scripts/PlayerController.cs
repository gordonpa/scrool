using UnityEngine;


 
public class PlayerController : MonoBehaviour {
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    bool canTurn = false;
    Vector3 startPosition;
    public static bool isDead = false;
    Rigidbody rb;

    public  GameObject magic;
    public Transform magicStartPos;
    Rigidbody mRb;

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall") {
            anim.SetTrigger("isDead");
            isDead = true;
        } else
            currentPlatform = other.gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        mRb = magic.GetComponent<Rigidbody>();

        player = this.gameObject;
        startPosition = player.transform.position;
        GenerateWorld.RunDummy();
    }

    void CastMagic()
    {
        // 将 magic 对象的位置设置到 magicStartPos 的位置
        magic.transform.position = magicStartPos.position;

        // 设置 magic 对象为激活状态
        magic.SetActive(true);
        mRb.velocity = Vector3.zero;
        mRb.angularVelocity = Vector3.zero;

        // 为 magic 对象的刚体添加力，使其沿玩家的 forward 方向飞行
        mRb.AddForce(this.transform.forward * 40000);

        // 调用 KillMagic 方法，在 1 秒后将 magic 对象停用
        Invoke("KillMagic", 1);
    }



    void KillMagic() {
        magic.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    void OnTriggerExit(Collider other) {
        if (other is SphereCollider)
            canTurn = false;
    }

    void StopJump() {
        anim.SetBool("isJumping", false);
    }

    void StopMagic() {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update() {
        if (PlayerController.isDead) return;


        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isMagic") == false) {
            anim.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * 200);

        } else if (Input.GetKeyDown(KeyCode.M) && anim.GetBool("isJumping") == false) {
            anim.SetBool("isMagic", true);
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                                this.transform.position.y,
                                                startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn) {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveller.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x,
                                    this.transform.position.y,
                                    startPosition.z);
        } else if (Input.GetKeyDown(KeyCode.A)) {
            this.transform.Translate(-0.5f, 0, 0);
        } else if (Input.GetKeyDown(KeyCode.D)) {
            this.transform.Translate(0.5f, 0, 0);
        }
    }
}

