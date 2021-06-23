using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public delegate void PlayerDataHandler(PlayerData playerData);
    public PlayerDataHandler OnPlayerDataUpdate;

    public delegate void ArrowInventoryHandler( Dictionary<ObjectsTag, float> arrowInventory);
    public ArrowInventoryHandler OnUpdateInventory;

    [SerializeField] private Transform _shootPosition;

    [SerializeField] private GameObject[] _bowArrows; 

    [SerializeField] private float _shootForce; 

    [SerializeField] private ObjectsTag _startArrow;

    private bool _isPaused = false;
    private bool _isAiming = false;
    private bool _isShooting = false;
    private bool _isChangingArrow = false;

    private ObjectsTag _selectedArrow;

    private ObjectPooler _objectPooler;

    private PlayerData _playerData;

    private Dictionary<ObjectsTag, float> _arrowInventory;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CreatePlayerStruct();
        _playerData = new PlayerData();
    }

    private void OnDestroy()
    {
        RemoveDelegates();
    }

    private void SetupDelegates()
    {
        GameManager.sInstance.InputListener.OnInput += ReceiveInputs;
        ArrowCollectable.OnCollectArrow += CollectArrow;
    }

    private void RemoveDelegates()
    {
        GameManager.sInstance.InputListener.OnInput -= ReceiveInputs;
        ArrowCollectable.OnCollectArrow -= CollectArrow;
    }

    private void Initialize()
    {
        SetupDelegates();
        _objectPooler = GameManager.sInstance.ObjectPooler;
        _arrowInventory = new Dictionary<ObjectsTag, float>();
        _arrowInventory.Add(ObjectsTag.PiercingArrow, 0);
        _arrowInventory.Add(ObjectsTag.ExplosiveArrow, 0);
        _selectedArrow = _startArrow;

       OnUpdateInventory?.Invoke(_arrowInventory);
    }

    private void PauseInputs(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void ReceiveInputs(InputData inputData)
    {
        if (!_isPaused)
        {
            AimState(inputData.Aim);
            ShootState(inputData.Shoot);
            ArrowSelection(inputData.Arrow);
        }
    }

    private void CreatePlayerStruct()
    {
        _playerData.Aim = _isAiming;
        _playerData.Shoot = _isShooting;
        _playerData.ChangeArrow = _isChangingArrow;

        if(_isShooting)
        {
            _isShooting = false;
        }
        if(_isChangingArrow)
        {
            _isChangingArrow = false;
        }

        OnPlayerDataUpdate?.Invoke(_playerData);
    }

    private void CollectArrow(ObjectsTag objectTag, float arrowAmount)
    {
        _arrowInventory[objectTag] += arrowAmount;
        OnUpdateInventory?.Invoke(_arrowInventory); //update hud
    }

    private void ArrowSelection(ObjectsTag arrow) //select arrow with keyboard input
    { 
        if(_selectedArrow != arrow) //if a different arrow is selected
        {
            if(arrow != _startArrow)
            {
                if(_arrowInventory[arrow] > 0) //check if is available in the inventory
                {
                    _isChangingArrow = true; //set changing arrow to true to play chaging arrow animation
                    _selectedArrow = arrow; //update selected arrow
                }
            }
            else
            {
                _isChangingArrow = true;
                _selectedArrow = arrow;
            }
        }
    }

    private void EquipArrow() //animation event
    {
        switch(_selectedArrow)
            {
                case ObjectsTag.RegularArrow:
                    _bowArrows[0].SetActive(true);
                    _bowArrows[1].SetActive(false);
                    _bowArrows[2].SetActive(false);
                    break;
                case ObjectsTag.PiercingArrow:
                    _bowArrows[0].SetActive(false);
                    _bowArrows[1].SetActive(true);
                    _bowArrows[2].SetActive(false);
                    break;
                case ObjectsTag.ExplosiveArrow:
                    _bowArrows[0].SetActive(false);
                    _bowArrows[1].SetActive(false);
                    _bowArrows[2].SetActive(true);
                    break;
            }
    }

    private void AimState(bool isAiming)
    {
        _isAiming = isAiming;
    }

    private void ShootState(bool isShooting) 
    {
        _isShooting = isShooting;
    }

    private void ArrowShoot() //animation event
    {   
        GameObject obj = _objectPooler.SpawnFromPool(_selectedArrow);

        if(_selectedArrow != _startArrow)
        {
            _arrowInventory[_selectedArrow]--;

            OnUpdateInventory?.Invoke(_arrowInventory); //update ui inventory

            if(_arrowInventory[_selectedArrow] <= 0) //if arrow inventory is empty after shoot, return to start arrow
            {
                _selectedArrow = _startArrow;
            }
        }

        obj.transform.position = _shootPosition.transform.position;
        obj.transform.rotation = _shootPosition.transform.rotation; 

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = (_shootPosition.transform.forward * _shootForce);
    }

}
