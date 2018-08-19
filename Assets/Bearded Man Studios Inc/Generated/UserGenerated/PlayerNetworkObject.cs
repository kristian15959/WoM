using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		private float _speed;
		public event FieldEvent<float> speedChanged;
		public InterpolateFloat speedInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float speed
		{
			get { return _speed; }
			set
			{
				// Don't do anything if the value is the same
				if (_speed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_speed = value;
				hasDirtyFields = true;
			}
		}

		public void SetspeedDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_speed(ulong timestep)
		{
			if (speedChanged != null) speedChanged(_speed, timestep);
			if (fieldAltered != null) fieldAltered("speed", _speed, timestep);
		}
		private float _animationSpeed;
		public event FieldEvent<float> animationSpeedChanged;
		public InterpolateFloat animationSpeedInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float animationSpeed
		{
			get { return _animationSpeed; }
			set
			{
				// Don't do anything if the value is the same
				if (_animationSpeed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_animationSpeed = value;
				hasDirtyFields = true;
			}
		}

		public void SetanimationSpeedDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_animationSpeed(ulong timestep)
		{
			if (animationSpeedChanged != null) animationSpeedChanged(_animationSpeed, timestep);
			if (fieldAltered != null) fieldAltered("animationSpeed", _animationSpeed, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			speedInterpolation.current = speedInterpolation.target;
			animationSpeedInterpolation.current = animationSpeedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _speed);
			UnityObjectMapper.Instance.MapBytes(data, _animationSpeed);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_speed = UnityObjectMapper.Instance.Map<float>(payload);
			speedInterpolation.current = _speed;
			speedInterpolation.target = _speed;
			RunChange_speed(timestep);
			_animationSpeed = UnityObjectMapper.Instance.Map<float>(payload);
			animationSpeedInterpolation.current = _animationSpeed;
			animationSpeedInterpolation.target = _animationSpeed;
			RunChange_animationSpeed(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _speed);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _animationSpeed);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (speedInterpolation.Enabled)
				{
					speedInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					speedInterpolation.Timestep = timestep;
				}
				else
				{
					_speed = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_speed(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (animationSpeedInterpolation.Enabled)
				{
					animationSpeedInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					animationSpeedInterpolation.Timestep = timestep;
				}
				else
				{
					_animationSpeed = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_animationSpeed(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (speedInterpolation.Enabled && !speedInterpolation.current.UnityNear(speedInterpolation.target, 0.0015f))
			{
				_speed = (float)speedInterpolation.Interpolate();
				//RunChange_speed(speedInterpolation.Timestep);
			}
			if (animationSpeedInterpolation.Enabled && !animationSpeedInterpolation.current.UnityNear(animationSpeedInterpolation.target, 0.0015f))
			{
				_animationSpeed = (float)animationSpeedInterpolation.Interpolate();
				//RunChange_animationSpeed(animationSpeedInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
