using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private float minForceImpulse = 9.5f, maxForceImpulse = 12.5f, forceRotation = 90.0f;
    private float spawnPosY = -0.2f;
    private float spawnPosX = 4.0f;
    private Rigidbody _rigidbody;
    private GameManager gameManager;
    [Range(-100,100), Tooltip("Puntos que sumará el gameObject")]public int pointValue;

    public ParticleSystem explosionParticle;
    public ParticleSystem impulseExplosion;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(RandomForce(),ForceMode.Impulse);
        _rigidbody.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition(); //Two parameters defined with z=0;
        Vector3 impulseExplosionPos = new Vector3(transform.position.x, spawnPosY, transform.position.z);
        Instantiate(impulseExplosion, impulseExplosionPos, Quaternion.identity);
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }

    /// <summary>
    /// Genera un vector aleatorio en 3D 
    /// </summary>
    /// <returns>Fuerza aleatoria hacia arriba</returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * (Random.Range(minForceImpulse, maxForceImpulse));
    }
    
    /// <summary>
    /// Genera un número float aleatorio
    /// </summary>
    /// <returns> Valor aleatorio entre (-forceRotation, forceRotation)</returns>
    private float RandomTorque()
    {
        return Random.Range(-forceRotation, forceRotation);
    }

    /// <summary>
    /// Genera una posición aleatoria
    /// </summary>
    /// <returns>Posición aleatoria en 3D con la coordenada z=0</returns>
    private Vector3 RandomSpawnPosition()
    {
        return  new Vector3(Random.Range(-(spawnPosX),spawnPosX), spawnPosY);
    }

    private void OnMouseOver()
    {
        if (gameManager.gameState == GameManager.GameState.inGame)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("Good"))
            {
              gameManager.GameOver();  
            }
           
        }
    }


}
