using UnityEngine;
using Mirror;

public class PlayerResources : NetworkBehaviour { 

    void Start() {
        this.setSpriteColor(this.color);
    }

    [SyncVar]
    private int id = -1;

    [SyncVar]
    private Color color = Color.white; 

    [SyncVar]
    private int hitpoints = 1; //Just set some values in case, so netowrk dosent throw errors

    [SyncVar]
    private int baseHitpoints = 1; 

    [SyncVar]
    private int movementspeed = 1;

    [SyncVar]
    private int baseMovementspeed = 1;

    [SyncVar]
    private int maxMovementspeed = 50;

    [SyncVar]
    private float rotationspeed = 1f;

    [SyncVar]
    private float baseRotationspeed = 1f;

    [SyncVar]
    private float maxRotationspeed = 1000f;

    [SyncVar]
    public ConnectionResources cr;

    /**
    * All SyncVar settings have to happen on the server
    * Ie. You have to have Server rights when calling SET the methods
    * GET methods are always available since SyncVar auto updates the values when they are set on the server
    *
    * Server rights can be ensured by using a server only method or a command call
    */

    public int getPlayerId() {
        return this.id;
    }

    public ConnectionResources getConnectionResources() {
        return this.cr;
    }

    [Server]
    public void setConnectionResources(ConnectionResources cr) {
       this.cr = cr;
    }

    [Server]
    public void setPlayerId(int playerId) {
        this.id = playerId;
    }

    public bool isOwner() {
        return this.hasAuthority;
    }

    [Server]
    public void setColor(Color c) {
        this.color = c;
    }

    public Color getColor() {
        return this.color;
    }

    public void setSpriteColor(Color c) {
        this.GetComponentInChildren<SpriteRenderer>().material.color = c;
    }

    public Color getSpriteColor() {
        return this.GetComponentInChildren<SpriteRenderer>().material.color;
    }


    [Server] //Only use when spawning
    public void initHitpoints(int hitpoints) {
        this.hitpoints = hitpoints;
        this.baseHitpoints = hitpoints;
    }


    /**
    * Return the inited value of the variable
    * Can be used to restore variable to original value
    */
    public int getBaseHitpoints() {
        return this.baseHitpoints;
    }

    [Server]
    public void setHitpoints(int hitpoints) {
        this.hitpoints = hitpoints;
    }

    public int getHitpoints() {
        return this.hitpoints;
    }


    [Server]
    public void reduceHitpoints(int hitAmount) {
        this.hitpoints = hitpoints - hitAmount;
    }

    [Server]
    public void increaseHitpoints(int healAmount) {
        this.hitpoints = hitpoints + healAmount;
        if(this.hitpoints > this.baseHitpoints) {
            this.hitpoints = this.baseHitpoints;
        }
    }

    public bool isDead() {
        return (this.hitpoints <= 0);
    }

    [Server] //Only use when spawning
    public void initMovementSpeed(int ms, int maxMs) {
        this.movementspeed = ms;
        this.baseMovementspeed = ms;
    }

    /**
    * Return the inited value of the variable
    * Can be used to restore variable to original value
    */
    public int getBaseMovementSpeed() {
        return this.baseMovementspeed;
    }

    /**
    * Return the inited max value of the variable
    * Value is automatically capped in setters
    */
    public int getMaxMovementSpeed() {
        return this.baseMovementspeed;
    }

    [Server]
    public void setMovementSpeed(int ms) {
        this.movementspeed = ms;

        if(this.movementspeed < 0) {
            this.movementspeed = 0;
        }
        else if(this.movementspeed > this.maxMovementspeed) {
            this.movementspeed = this.maxMovementspeed;
        }
    }

    public int getMovementSpeed() {
        return this.movementspeed;
    }

    [Server]
    public void reduceMovementSpeed(int slowAmount) {
        this.movementspeed = movementspeed - slowAmount;

        if(this.movementspeed < 0) {
            this.movementspeed = 0;
        }
    }

    [Server]
    public void increaseMovementSpeed(int boostAmount) {
        this.movementspeed = movementspeed + boostAmount;

        if(this.movementspeed > this.maxMovementspeed) {
            this.movementspeed = this.maxMovementspeed;
        }
    }

     [Server] //Only use when spawning
    public void initRotationSpeed(float rs, float maxRs) {
        this.rotationspeed= rs;
        this.baseRotationspeed = rs;
        this.maxRotationspeed = maxRs;
    }

    /**
    * Return the inited value of the variable
    * Can be used to restore variable to original value
    */
    public float getBaseRotationSpeed() {
        return this.baseRotationspeed;
    }

    /**
    * Return the inited max value of the variable
    * Value is automatically capped in setters
    */
    public float getMaxRotationSpeed() {
        return this.baseMovementspeed;
    }

    [Server]
    public void setRotationSpeed(float rs) {
        this.rotationspeed = rs;

        if(this.rotationspeed < 0f) {
            this.rotationspeed = 0f;
        }
        else if(this.rotationspeed > this.maxRotationspeed) {
            this.rotationspeed = this.maxRotationspeed;
        }
    }

    public float getRotationSpeed() {
        return this.rotationspeed;
    }

    [Server]
    public void reduceRotationSpeed(float slowAmount) {
        this.rotationspeed = rotationspeed - slowAmount;

        if(this.rotationspeed < 0f) {
            this.rotationspeed = 0f;
        }
    }

    [Server]
    public void increaseRotationSpeed(float boostAmount) {
        this.rotationspeed = rotationspeed + boostAmount;

        if(this.rotationspeed > this.maxRotationspeed) {
            this.rotationspeed = this.maxRotationspeed;
        }
    }
}