var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i316 = root || request.c( 'UnityEngine.JointSpring' )
  var i317 = data
  i316.spring = i317[0]
  i316.damper = i317[1]
  i316.targetPosition = i317[2]
  return i316
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i318 = root || request.c( 'UnityEngine.JointMotor' )
  var i319 = data
  i318.m_TargetVelocity = i319[0]
  i318.m_Force = i319[1]
  i318.m_FreeSpin = i319[2]
  return i318
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i320 = root || request.c( 'UnityEngine.JointLimits' )
  var i321 = data
  i320.m_Min = i321[0]
  i320.m_Max = i321[1]
  i320.m_Bounciness = i321[2]
  i320.m_BounceMinVelocity = i321[3]
  i320.m_ContactDistance = i321[4]
  i320.minBounce = i321[5]
  i320.maxBounce = i321[6]
  return i320
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i322 = root || request.c( 'UnityEngine.JointDrive' )
  var i323 = data
  i322.m_PositionSpring = i323[0]
  i322.m_PositionDamper = i323[1]
  i322.m_MaximumForce = i323[2]
  i322.m_UseAcceleration = i323[3]
  return i322
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i324 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i325 = data
  i324.m_Spring = i325[0]
  i324.m_Damper = i325[1]
  return i324
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i326 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i327 = data
  i326.m_Limit = i327[0]
  i326.m_Bounciness = i327[1]
  i326.m_ContactDistance = i327[2]
  return i326
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i328 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i329 = data
  i328.m_ExtremumSlip = i329[0]
  i328.m_ExtremumValue = i329[1]
  i328.m_AsymptoteSlip = i329[2]
  i328.m_AsymptoteValue = i329[3]
  i328.m_Stiffness = i329[4]
  return i328
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i330 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i331 = data
  i330.m_LowerAngle = i331[0]
  i330.m_UpperAngle = i331[1]
  return i330
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i332 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i333 = data
  i332.m_MotorSpeed = i333[0]
  i332.m_MaximumMotorTorque = i333[1]
  return i332
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i334 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i335 = data
  i334.m_DampingRatio = i335[0]
  i334.m_Frequency = i335[1]
  i334.m_Angle = i335[2]
  return i334
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i336 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i337 = data
  i336.m_LowerTranslation = i337[0]
  i336.m_UpperTranslation = i337[1]
  return i336
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i338 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i339 = data
  i338.position = new pc.Vec3( i339[0], i339[1], i339[2] )
  i338.scale = new pc.Vec3( i339[3], i339[4], i339[5] )
  i338.rotation = new pc.Quat(i339[6], i339[7], i339[8], i339[9])
  return i338
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i340 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i341 = data
  i340.type = i341[0]
  i340.color = new pc.Color(i341[1], i341[2], i341[3], i341[4])
  i340.cullingMask = i341[5]
  i340.intensity = i341[6]
  i340.range = i341[7]
  i340.spotAngle = i341[8]
  i340.shadows = i341[9]
  i340.shadowNormalBias = i341[10]
  i340.shadowBias = i341[11]
  i340.shadowStrength = i341[12]
  i340.shadowResolution = i341[13]
  i340.lightmapBakeType = i341[14]
  i340.renderMode = i341[15]
  request.r(i341[16], i341[17], 0, i340, 'cookie')
  i340.cookieSize = i341[18]
  i340.enabled = !!i341[19]
  return i340
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i342 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i343 = data
  i342.name = i343[0]
  i342.tagId = i343[1]
  i342.enabled = !!i343[2]
  i342.isStatic = !!i343[3]
  i342.layer = i343[4]
  return i342
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i344 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i345 = data
  i344.aspect = i345[0]
  i344.orthographic = !!i345[1]
  i344.orthographicSize = i345[2]
  i344.backgroundColor = new pc.Color(i345[3], i345[4], i345[5], i345[6])
  i344.nearClipPlane = i345[7]
  i344.farClipPlane = i345[8]
  i344.fieldOfView = i345[9]
  i344.depth = i345[10]
  i344.clearFlags = i345[11]
  i344.cullingMask = i345[12]
  i344.rect = i345[13]
  request.r(i345[14], i345[15], 0, i344, 'targetTexture')
  i344.usePhysicalProperties = !!i345[16]
  i344.focalLength = i345[17]
  i344.sensorSize = new pc.Vec2( i345[18], i345[19] )
  i344.lensShift = new pc.Vec2( i345[20], i345[21] )
  i344.gateFit = i345[22]
  i344.commandBufferCount = i345[23]
  i344.cameraType = i345[24]
  i344.enabled = !!i345[25]
  return i344
}

Deserializers["Project.Scripts.CameraManagement.CameraManager"] = function (request, data, root) {
  var i346 = root || request.c( 'Project.Scripts.CameraManagement.CameraManager' )
  var i347 = data
  request.r(i347[0], i347[1], 0, i346, 'm_cameraSettings')
  return i346
}

Deserializers["Cinemachine.CinemachineBrain"] = function (request, data, root) {
  var i348 = root || request.c( 'Cinemachine.CinemachineBrain' )
  var i349 = data
  i348.m_ShowDebugText = !!i349[0]
  i348.m_ShowCameraFrustum = !!i349[1]
  i348.m_IgnoreTimeScale = !!i349[2]
  request.r(i349[3], i349[4], 0, i348, 'm_WorldUpOverride')
  i348.m_UpdateMethod = i349[5]
  i348.m_BlendUpdateMethod = i349[6]
  i348.m_DefaultBlend = request.d('Cinemachine.CinemachineBlendDefinition', i349[7], i348.m_DefaultBlend)
  request.r(i349[8], i349[9], 0, i348, 'm_CustomBlends')
  i348.m_CameraCutEvent = request.d('Cinemachine.CinemachineBrain+BrainEvent', i349[10], i348.m_CameraCutEvent)
  i348.m_CameraActivatedEvent = request.d('Cinemachine.CinemachineBrain+VcamActivatedEvent', i349[11], i348.m_CameraActivatedEvent)
  return i348
}

Deserializers["Cinemachine.CinemachineBlendDefinition"] = function (request, data, root) {
  var i350 = root || request.c( 'Cinemachine.CinemachineBlendDefinition' )
  var i351 = data
  i350.m_Style = i351[0]
  i350.m_Time = i351[1]
  i350.m_CustomCurve = new pc.AnimationCurve( { keys_flow: i351[2] } )
  return i350
}

Deserializers["Cinemachine.CinemachineBrain+BrainEvent"] = function (request, data, root) {
  var i352 = root || request.c( 'Cinemachine.CinemachineBrain+BrainEvent' )
  var i353 = data
  i352.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i353[0], i352.m_PersistentCalls)
  return i352
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i354 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i355 = data
  var i357 = i355[0]
  var i356 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i357.length; i += 1) {
    i356.add(request.d('UnityEngine.Events.PersistentCall', i357[i + 0]));
  }
  i354.m_Calls = i356
  return i354
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i360 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i361 = data
  request.r(i361[0], i361[1], 0, i360, 'm_Target')
  i360.m_TargetAssemblyTypeName = i361[2]
  i360.m_MethodName = i361[3]
  i360.m_Mode = i361[4]
  i360.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i361[5], i360.m_Arguments)
  i360.m_CallState = i361[6]
  return i360
}

Deserializers["Cinemachine.CinemachineBrain+VcamActivatedEvent"] = function (request, data, root) {
  var i362 = root || request.c( 'Cinemachine.CinemachineBrain+VcamActivatedEvent' )
  var i363 = data
  i362.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i363[0], i362.m_PersistentCalls)
  return i362
}

Deserializers["Cinemachine.CinemachineVirtualCamera"] = function (request, data, root) {
  var i364 = root || request.c( 'Cinemachine.CinemachineVirtualCamera' )
  var i365 = data
  request.r(i365[0], i365[1], 0, i364, 'm_LookAt')
  request.r(i365[2], i365[3], 0, i364, 'm_Follow')
  i364.m_Lens = request.d('Cinemachine.LensSettings', i365[4], i364.m_Lens)
  i364.m_Transitions = request.d('Cinemachine.CinemachineVirtualCameraBase+TransitionParams', i365[5], i364.m_Transitions)
  var i367 = i365[6]
  var i366 = []
  for(var i = 0; i < i367.length; i += 1) {
    i366.push( i367[i + 0] );
  }
  i364.m_ExcludedPropertiesInInspector = i366
  var i369 = i365[7]
  var i368 = []
  for(var i = 0; i < i369.length; i += 1) {
    i368.push( i369[i + 0] );
  }
  i364.m_LockStageInInspector = i368
  i364.m_Priority = i365[8]
  i364.m_StandbyUpdate = i365[9]
  i364.m_LegacyBlendHint = i365[10]
  request.r(i365[11], i365[12], 0, i364, 'm_ComponentOwner')
  i364.m_StreamingVersion = i365[13]
  return i364
}

Deserializers["Cinemachine.LensSettings"] = function (request, data, root) {
  var i370 = root || request.c( 'Cinemachine.LensSettings' )
  var i371 = data
  i370.FieldOfView = i371[0]
  i370.OrthographicSize = i371[1]
  i370.NearClipPlane = i371[2]
  i370.FarClipPlane = i371[3]
  i370.Dutch = i371[4]
  i370.ModeOverride = i371[5]
  i370.LensShift = new pc.Vec2( i371[6], i371[7] )
  i370.GateFit = i371[8]
  i370.FocusDistance = i371[9]
  i370.m_SensorSize = new pc.Vec2( i371[10], i371[11] )
  return i370
}

Deserializers["Cinemachine.CinemachineVirtualCameraBase+TransitionParams"] = function (request, data, root) {
  var i372 = root || request.c( 'Cinemachine.CinemachineVirtualCameraBase+TransitionParams' )
  var i373 = data
  i372.m_BlendHint = i373[0]
  i372.m_InheritPosition = !!i373[1]
  i372.m_OnCameraLive = request.d('Cinemachine.CinemachineBrain+VcamActivatedEvent', i373[2], i372.m_OnCameraLive)
  return i372
}

Deserializers["Cinemachine.CinemachinePipeline"] = function (request, data, root) {
  var i378 = root || request.c( 'Cinemachine.CinemachinePipeline' )
  var i379 = data
  return i378
}

Deserializers["Cinemachine.CinemachineFramingTransposer"] = function (request, data, root) {
  var i380 = root || request.c( 'Cinemachine.CinemachineFramingTransposer' )
  var i381 = data
  i380.m_TrackedObjectOffset = new pc.Vec3( i381[0], i381[1], i381[2] )
  i380.m_LookaheadTime = i381[3]
  i380.m_LookaheadSmoothing = i381[4]
  i380.m_LookaheadIgnoreY = !!i381[5]
  i380.m_XDamping = i381[6]
  i380.m_YDamping = i381[7]
  i380.m_ZDamping = i381[8]
  i380.m_TargetMovementOnly = !!i381[9]
  i380.m_ScreenX = i381[10]
  i380.m_ScreenY = i381[11]
  i380.m_CameraDistance = i381[12]
  i380.m_DeadZoneWidth = i381[13]
  i380.m_DeadZoneHeight = i381[14]
  i380.m_DeadZoneDepth = i381[15]
  i380.m_UnlimitedSoftZone = !!i381[16]
  i380.m_SoftZoneWidth = i381[17]
  i380.m_SoftZoneHeight = i381[18]
  i380.m_BiasX = i381[19]
  i380.m_BiasY = i381[20]
  i380.m_CenterOnActivate = !!i381[21]
  i380.m_GroupFramingMode = i381[22]
  i380.m_AdjustmentMode = i381[23]
  i380.m_GroupFramingSize = i381[24]
  i380.m_MaxDollyIn = i381[25]
  i380.m_MaxDollyOut = i381[26]
  i380.m_MinimumDistance = i381[27]
  i380.m_MaximumDistance = i381[28]
  i380.m_MinimumFOV = i381[29]
  i380.m_MaximumFOV = i381[30]
  i380.m_MinimumOrthoSize = i381[31]
  i380.m_MaximumOrthoSize = i381[32]
  return i380
}

Deserializers["Project.Scripts.GameManager"] = function (request, data, root) {
  var i382 = root || request.c( 'Project.Scripts.GameManager' )
  var i383 = data
  return i382
}

Deserializers["Project.Scripts.LevelManagement.LevelManager"] = function (request, data, root) {
  var i384 = root || request.c( 'Project.Scripts.LevelManagement.LevelManager' )
  var i385 = data
  request.r(i385[0], i385[1], 0, i384, 'm_levelSettings')
  request.r(i385[2], i385[3], 0, i384, 'm_levelParent')
  return i384
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer"] = function (request, data, root) {
  var i386 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer' )
  var i387 = data
  i386.color = new pc.Color(i387[0], i387[1], i387[2], i387[3])
  request.r(i387[4], i387[5], 0, i386, 'sprite')
  i386.flipX = !!i387[6]
  i386.flipY = !!i387[7]
  i386.drawMode = i387[8]
  i386.size = new pc.Vec2( i387[9], i387[10] )
  i386.tileMode = i387[11]
  i386.adaptiveModeThreshold = i387[12]
  i386.maskInteraction = i387[13]
  i386.spriteSortPoint = i387[14]
  i386.enabled = !!i387[15]
  request.r(i387[16], i387[17], 0, i386, 'sharedMaterial')
  var i389 = i387[18]
  var i388 = []
  for(var i = 0; i < i389.length; i += 2) {
  request.r(i389[i + 0], i389[i + 1], 2, i388, '')
  }
  i386.sharedMaterials = i388
  i386.receiveShadows = !!i387[19]
  i386.shadowCastingMode = i387[20]
  i386.sortingLayerID = i387[21]
  i386.sortingOrder = i387[22]
  i386.lightmapIndex = i387[23]
  i386.lightmapSceneIndex = i387[24]
  i386.lightmapScaleOffset = new pc.Vec4( i387[25], i387[26], i387[27], i387[28] )
  i386.lightProbeUsage = i387[29]
  i386.reflectionProbeUsage = i387[30]
  return i386
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i392 = root || new pc.UnityMaterial()
  var i393 = data
  i392.name = i393[0]
  request.r(i393[1], i393[2], 0, i392, 'shader')
  i392.renderQueue = i393[3]
  i392.enableInstancing = !!i393[4]
  var i395 = i393[5]
  var i394 = []
  for(var i = 0; i < i395.length; i += 1) {
    i394.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i395[i + 0]) );
  }
  i392.floatParameters = i394
  var i397 = i393[6]
  var i396 = []
  for(var i = 0; i < i397.length; i += 1) {
    i396.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i397[i + 0]) );
  }
  i392.colorParameters = i396
  var i399 = i393[7]
  var i398 = []
  for(var i = 0; i < i399.length; i += 1) {
    i398.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i399[i + 0]) );
  }
  i392.vectorParameters = i398
  var i401 = i393[8]
  var i400 = []
  for(var i = 0; i < i401.length; i += 1) {
    i400.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i401[i + 0]) );
  }
  i392.textureParameters = i400
  var i403 = i393[9]
  var i402 = []
  for(var i = 0; i < i403.length; i += 1) {
    i402.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i403[i + 0]) );
  }
  i392.materialFlags = i402
  return i392
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i406 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i407 = data
  i406.name = i407[0]
  i406.value = i407[1]
  return i406
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i410 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i411 = data
  i410.name = i411[0]
  i410.value = new pc.Color(i411[1], i411[2], i411[3], i411[4])
  return i410
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i414 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i415 = data
  i414.name = i415[0]
  i414.value = new pc.Vec4( i415[1], i415[2], i415[3], i415[4] )
  return i414
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i418 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i419 = data
  i418.name = i419[0]
  request.r(i419[1], i419[2], 0, i418, 'value')
  return i418
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i422 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i423 = data
  i422.name = i423[0]
  i422.enabled = !!i423[1]
  return i422
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i424 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i425 = data
  i424.name = i425[0]
  i424.width = i425[1]
  i424.height = i425[2]
  i424.mipmapCount = i425[3]
  i424.anisoLevel = i425[4]
  i424.filterMode = i425[5]
  i424.hdr = !!i425[6]
  i424.format = i425[7]
  i424.wrapMode = i425[8]
  i424.alphaIsTransparency = !!i425[9]
  i424.alphaSource = i425[10]
  i424.graphicsFormat = i425[11]
  i424.sRGBTexture = !!i425[12]
  i424.desiredColorSpace = i425[13]
  i424.wrapU = i425[14]
  i424.wrapV = i425[15]
  return i424
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D"] = function (request, data, root) {
  var i426 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D' )
  var i427 = data
  i426.usedByComposite = !!i427[0]
  i426.autoTiling = !!i427[1]
  i426.size = new pc.Vec2( i427[2], i427[3] )
  i426.edgeRadius = i427[4]
  i426.enabled = !!i427[5]
  i426.isTrigger = !!i427[6]
  i426.usedByEffector = !!i427[7]
  i426.density = i427[8]
  i426.offset = new pc.Vec2( i427[9], i427[10] )
  request.r(i427[11], i427[12], 0, i426, 'material')
  return i426
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.RectTransform"] = function (request, data, root) {
  var i428 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.RectTransform' )
  var i429 = data
  i428.pivot = new pc.Vec2( i429[0], i429[1] )
  i428.anchorMin = new pc.Vec2( i429[2], i429[3] )
  i428.anchorMax = new pc.Vec2( i429[4], i429[5] )
  i428.sizeDelta = new pc.Vec2( i429[6], i429[7] )
  i428.anchoredPosition3D = new pc.Vec3( i429[8], i429[9], i429[10] )
  i428.rotation = new pc.Quat(i429[11], i429[12], i429[13], i429[14])
  i428.scale = new pc.Vec3( i429[15], i429[16], i429[17] )
  return i428
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Canvas"] = function (request, data, root) {
  var i430 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Canvas' )
  var i431 = data
  i430.planeDistance = i431[0]
  i430.referencePixelsPerUnit = i431[1]
  i430.isFallbackOverlay = !!i431[2]
  i430.renderMode = i431[3]
  i430.renderOrder = i431[4]
  i430.sortingLayerName = i431[5]
  i430.sortingOrder = i431[6]
  i430.scaleFactor = i431[7]
  request.r(i431[8], i431[9], 0, i430, 'worldCamera')
  i430.overrideSorting = !!i431[10]
  i430.pixelPerfect = !!i431[11]
  i430.targetDisplay = i431[12]
  i430.overridePixelPerfect = !!i431[13]
  i430.enabled = !!i431[14]
  return i430
}

Deserializers["UnityEngine.UI.CanvasScaler"] = function (request, data, root) {
  var i432 = root || request.c( 'UnityEngine.UI.CanvasScaler' )
  var i433 = data
  i432.m_UiScaleMode = i433[0]
  i432.m_ReferencePixelsPerUnit = i433[1]
  i432.m_ScaleFactor = i433[2]
  i432.m_ReferenceResolution = new pc.Vec2( i433[3], i433[4] )
  i432.m_ScreenMatchMode = i433[5]
  i432.m_MatchWidthOrHeight = i433[6]
  i432.m_PhysicalUnit = i433[7]
  i432.m_FallbackScreenDPI = i433[8]
  i432.m_DefaultSpriteDPI = i433[9]
  i432.m_DynamicPixelsPerUnit = i433[10]
  i432.m_PresetInfoIsWorld = !!i433[11]
  return i432
}

Deserializers["UnityEngine.UI.GraphicRaycaster"] = function (request, data, root) {
  var i434 = root || request.c( 'UnityEngine.UI.GraphicRaycaster' )
  var i435 = data
  i434.m_IgnoreReversedGraphics = !!i435[0]
  i434.m_BlockingObjects = i435[1]
  i434.m_BlockingMask = UnityEngine.LayerMask.FromIntegerValue( i435[2] )
  return i434
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer"] = function (request, data, root) {
  var i436 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer' )
  var i437 = data
  i436.cullTransparentMesh = !!i437[0]
  return i436
}

Deserializers["UnityEngine.UI.Image"] = function (request, data, root) {
  var i438 = root || request.c( 'UnityEngine.UI.Image' )
  var i439 = data
  request.r(i439[0], i439[1], 0, i438, 'm_Sprite')
  i438.m_Type = i439[2]
  i438.m_PreserveAspect = !!i439[3]
  i438.m_FillCenter = !!i439[4]
  i438.m_FillMethod = i439[5]
  i438.m_FillAmount = i439[6]
  i438.m_FillClockwise = !!i439[7]
  i438.m_FillOrigin = i439[8]
  i438.m_UseSpriteMesh = !!i439[9]
  i438.m_PixelsPerUnitMultiplier = i439[10]
  i438.m_Maskable = !!i439[11]
  request.r(i439[12], i439[13], 0, i438, 'm_Material')
  i438.m_Color = new pc.Color(i439[14], i439[15], i439[16], i439[17])
  i438.m_RaycastTarget = !!i439[18]
  i438.m_RaycastPadding = new pc.Vec4( i439[19], i439[20], i439[21], i439[22] )
  return i438
}

Deserializers["DynamicJoystick"] = function (request, data, root) {
  var i440 = root || request.c( 'DynamicJoystick' )
  var i441 = data
  i440.moveThreshold = i441[0]
  i440.handleRange = i441[1]
  i440.deadZone = i441[2]
  i440.axisOptions = i441[3]
  i440.snapX = !!i441[4]
  i440.snapY = !!i441[5]
  request.r(i441[6], i441[7], 0, i440, 'background')
  request.r(i441[8], i441[9], 0, i440, 'joystickHandle')
  return i440
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Rigidbody2D"] = function (request, data, root) {
  var i442 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Rigidbody2D' )
  var i443 = data
  i442.bodyType = i443[0]
  request.r(i443[1], i443[2], 0, i442, 'material')
  i442.simulated = !!i443[3]
  i442.useAutoMass = !!i443[4]
  i442.mass = i443[5]
  i442.drag = i443[6]
  i442.angularDrag = i443[7]
  i442.gravityScale = i443[8]
  i442.collisionDetectionMode = i443[9]
  i442.sleepMode = i443[10]
  i442.constraints = i443[11]
  return i442
}

Deserializers["Project.Scripts.Entity.Player.PlayerEntity"] = function (request, data, root) {
  var i444 = root || request.c( 'Project.Scripts.Entity.Player.PlayerEntity' )
  var i445 = data
  return i444
}

Deserializers["Project.Scripts.Entity.Player.Movement.MovementSystem"] = function (request, data, root) {
  var i446 = root || request.c( 'Project.Scripts.Entity.Player.Movement.MovementSystem' )
  var i447 = data
  request.r(i447[0], i447[1], 0, i446, 'm_movementSettings')
  return i446
}

Deserializers["Project.Scripts.Entity.Player.Combat.CombatSystem"] = function (request, data, root) {
  var i448 = root || request.c( 'Project.Scripts.Entity.Player.Combat.CombatSystem' )
  var i449 = data
  request.r(i449[0], i449[1], 0, i448, 'm_combatSettings')
  request.r(i449[2], i449[3], 0, i448, 'm_weaponParent')
  return i448
}

Deserializers["Project.Scripts.CollisionManagement.CollisionBroadcaster2D"] = function (request, data, root) {
  var i450 = root || request.c( 'Project.Scripts.CollisionManagement.CollisionBroadcaster2D' )
  var i451 = data
  i450.m_layerMask = UnityEngine.LayerMask.FromIntegerValue( i451[0] )
  return i450
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.PolygonCollider2D"] = function (request, data, root) {
  var i452 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.PolygonCollider2D' )
  var i453 = data
  i452.usedByComposite = !!i453[0]
  i452.autoTiling = !!i453[1]
  var i455 = i453[2]
  var i454 = []
  for(var i = 0; i < i455.length; i += 1) {
  var i457 = i455[i + 0]
  var i456 = []
  for(var i = 0; i < i457.length; i += 2) {
    i456.push( new pc.Vec2( i457[i + 0], i457[i + 1] ) );
  }
    i454.push( i456 );
  }
  i452.points = i454
  i452.enabled = !!i453[3]
  i452.isTrigger = !!i453[4]
  i452.usedByEffector = !!i453[5]
  i452.density = i453[6]
  i452.offset = new pc.Vec2( i453[7], i453[8] )
  request.r(i453[9], i453[10], 0, i452, 'material')
  return i452
}

Deserializers["Project.Scripts.Entity.Sword.SwordEntity"] = function (request, data, root) {
  var i464 = root || request.c( 'Project.Scripts.Entity.Sword.SwordEntity' )
  var i465 = data
  return i464
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i466 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i467 = data
  i466.name = i467[0]
  i466.atlasId = i467[1]
  i466.mipmapCount = i467[2]
  i466.hdr = !!i467[3]
  i466.size = i467[4]
  i466.anisoLevel = i467[5]
  i466.filterMode = i467[6]
  var i469 = i467[7]
  var i468 = []
  for(var i = 0; i < i469.length; i += 4) {
    i468.push( UnityEngine.Rect.MinMaxRect(i469[i + 0], i469[i + 1], i469[i + 2], i469[i + 3]) );
  }
  i466.rects = i468
  i466.wrapU = i467[8]
  i466.wrapV = i467[9]
  return i466
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i472 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i473 = data
  i472.name = i473[0]
  i472.index = i473[1]
  i472.startup = !!i473[2]
  return i472
}

Deserializers["UnityEngine.EventSystems.EventSystem"] = function (request, data, root) {
  var i474 = root || request.c( 'UnityEngine.EventSystems.EventSystem' )
  var i475 = data
  request.r(i475[0], i475[1], 0, i474, 'm_FirstSelected')
  i474.m_sendNavigationEvents = !!i475[2]
  i474.m_DragThreshold = i475[3]
  return i474
}

Deserializers["UnityEngine.EventSystems.StandaloneInputModule"] = function (request, data, root) {
  var i476 = root || request.c( 'UnityEngine.EventSystems.StandaloneInputModule' )
  var i477 = data
  i476.m_HorizontalAxis = i477[0]
  i476.m_VerticalAxis = i477[1]
  i476.m_SubmitButton = i477[2]
  i476.m_CancelButton = i477[3]
  i476.m_InputActionsPerSecond = i477[4]
  i476.m_RepeatDelay = i477[5]
  i476.m_ForceModuleActive = !!i477[6]
  i476.m_SendPointerHoverToParent = !!i477[7]
  return i476
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i478 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i479 = data
  i478.ambientIntensity = i479[0]
  i478.reflectionIntensity = i479[1]
  i478.ambientMode = i479[2]
  i478.ambientLight = new pc.Color(i479[3], i479[4], i479[5], i479[6])
  i478.ambientSkyColor = new pc.Color(i479[7], i479[8], i479[9], i479[10])
  i478.ambientGroundColor = new pc.Color(i479[11], i479[12], i479[13], i479[14])
  i478.ambientEquatorColor = new pc.Color(i479[15], i479[16], i479[17], i479[18])
  i478.fogColor = new pc.Color(i479[19], i479[20], i479[21], i479[22])
  i478.fogEndDistance = i479[23]
  i478.fogStartDistance = i479[24]
  i478.fogDensity = i479[25]
  i478.fog = !!i479[26]
  request.r(i479[27], i479[28], 0, i478, 'skybox')
  i478.fogMode = i479[29]
  var i481 = i479[30]
  var i480 = []
  for(var i = 0; i < i481.length; i += 1) {
    i480.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i481[i + 0]) );
  }
  i478.lightmaps = i480
  i478.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i479[31], i478.lightProbes)
  i478.lightmapsMode = i479[32]
  i478.mixedBakeMode = i479[33]
  i478.environmentLightingMode = i479[34]
  i478.ambientProbe = new pc.SphericalHarmonicsL2(i479[35])
  i478.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i479[36])
  i478.useReferenceAmbientProbe = !!i479[37]
  request.r(i479[38], i479[39], 0, i478, 'customReflection')
  request.r(i479[40], i479[41], 0, i478, 'defaultReflection')
  i478.defaultReflectionMode = i479[42]
  i478.defaultReflectionResolution = i479[43]
  i478.sunLightObjectId = i479[44]
  i478.pixelLightCount = i479[45]
  i478.defaultReflectionHDR = !!i479[46]
  i478.hasLightDataAsset = !!i479[47]
  i478.hasManualGenerate = !!i479[48]
  return i478
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i484 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i485 = data
  request.r(i485[0], i485[1], 0, i484, 'lightmapColor')
  request.r(i485[2], i485[3], 0, i484, 'lightmapDirection')
  return i484
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i486 = root || new UnityEngine.LightProbes()
  var i487 = data
  return i486
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i494 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i495 = data
  var i497 = i495[0]
  var i496 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i497.length; i += 1) {
    i496.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i497[i + 0]));
  }
  i494.ShaderCompilationErrors = i496
  i494.name = i495[1]
  i494.guid = i495[2]
  var i499 = i495[3]
  var i498 = []
  for(var i = 0; i < i499.length; i += 1) {
    i498.push( i499[i + 0] );
  }
  i494.shaderDefinedKeywords = i498
  var i501 = i495[4]
  var i500 = []
  for(var i = 0; i < i501.length; i += 1) {
    i500.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i501[i + 0]) );
  }
  i494.passes = i500
  var i503 = i495[5]
  var i502 = []
  for(var i = 0; i < i503.length; i += 1) {
    i502.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i503[i + 0]) );
  }
  i494.usePasses = i502
  var i505 = i495[6]
  var i504 = []
  for(var i = 0; i < i505.length; i += 1) {
    i504.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i505[i + 0]) );
  }
  i494.defaultParameterValues = i504
  request.r(i495[7], i495[8], 0, i494, 'unityFallbackShader')
  i494.readDepth = !!i495[9]
  i494.isCreatedByShaderGraph = !!i495[10]
  i494.disableBatching = !!i495[11]
  i494.compiled = !!i495[12]
  return i494
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i508 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i509 = data
  i508.shaderName = i509[0]
  i508.errorMessage = i509[1]
  return i508
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i512 = root || new pc.UnityShaderPass()
  var i513 = data
  i512.id = i513[0]
  i512.subShaderIndex = i513[1]
  i512.name = i513[2]
  i512.passType = i513[3]
  i512.grabPassTextureName = i513[4]
  i512.usePass = !!i513[5]
  i512.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[6], i512.zTest)
  i512.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[7], i512.zWrite)
  i512.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[8], i512.culling)
  i512.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i513[9], i512.blending)
  i512.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i513[10], i512.alphaBlending)
  i512.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[11], i512.colorWriteMask)
  i512.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[12], i512.offsetUnits)
  i512.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[13], i512.offsetFactor)
  i512.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[14], i512.stencilRef)
  i512.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[15], i512.stencilReadMask)
  i512.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i513[16], i512.stencilWriteMask)
  i512.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i513[17], i512.stencilOp)
  i512.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i513[18], i512.stencilOpFront)
  i512.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i513[19], i512.stencilOpBack)
  var i515 = i513[20]
  var i514 = []
  for(var i = 0; i < i515.length; i += 1) {
    i514.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i515[i + 0]) );
  }
  i512.tags = i514
  var i517 = i513[21]
  var i516 = []
  for(var i = 0; i < i517.length; i += 1) {
    i516.push( i517[i + 0] );
  }
  i512.passDefinedKeywords = i516
  var i519 = i513[22]
  var i518 = []
  for(var i = 0; i < i519.length; i += 1) {
    i518.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i519[i + 0]) );
  }
  i512.passDefinedKeywordGroups = i518
  var i521 = i513[23]
  var i520 = []
  for(var i = 0; i < i521.length; i += 1) {
    i520.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i521[i + 0]) );
  }
  i512.variants = i520
  var i523 = i513[24]
  var i522 = []
  for(var i = 0; i < i523.length; i += 1) {
    i522.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i523[i + 0]) );
  }
  i512.excludedVariants = i522
  i512.hasDepthReader = !!i513[25]
  return i512
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i524 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i525 = data
  i524.val = i525[0]
  i524.name = i525[1]
  return i524
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i526 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i527 = data
  i526.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i527[0], i526.src)
  i526.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i527[1], i526.dst)
  i526.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i527[2], i526.op)
  return i526
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i528 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i529 = data
  i528.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i529[0], i528.pass)
  i528.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i529[1], i528.fail)
  i528.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i529[2], i528.zFail)
  i528.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i529[3], i528.comp)
  return i528
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i532 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i533 = data
  i532.name = i533[0]
  i532.value = i533[1]
  return i532
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i536 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i537 = data
  var i539 = i537[0]
  var i538 = []
  for(var i = 0; i < i539.length; i += 1) {
    i538.push( i539[i + 0] );
  }
  i536.keywords = i538
  i536.hasDiscard = !!i537[1]
  return i536
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i542 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i543 = data
  i542.passId = i543[0]
  i542.subShaderIndex = i543[1]
  var i545 = i543[2]
  var i544 = []
  for(var i = 0; i < i545.length; i += 1) {
    i544.push( i545[i + 0] );
  }
  i542.keywords = i544
  i542.vertexProgram = i543[3]
  i542.fragmentProgram = i543[4]
  i542.exportedForWebGl2 = !!i543[5]
  i542.readDepth = !!i543[6]
  return i542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i548 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i549 = data
  request.r(i549[0], i549[1], 0, i548, 'shader')
  i548.pass = i549[2]
  return i548
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i552 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i553 = data
  i552.name = i553[0]
  i552.type = i553[1]
  i552.value = new pc.Vec4( i553[2], i553[3], i553[4], i553[5] )
  i552.textureValue = i553[6]
  i552.shaderPropertyFlag = i553[7]
  return i552
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Sprite"] = function (request, data, root) {
  var i554 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Sprite' )
  var i555 = data
  i554.name = i555[0]
  request.r(i555[1], i555[2], 0, i554, 'texture')
  i554.aabb = i555[3]
  i554.vertices = i555[4]
  i554.triangles = i555[5]
  i554.textureRect = UnityEngine.Rect.MinMaxRect(i555[6], i555[7], i555[8], i555[9])
  i554.packedRect = UnityEngine.Rect.MinMaxRect(i555[10], i555[11], i555[12], i555[13])
  i554.border = new pc.Vec4( i555[14], i555[15], i555[16], i555[17] )
  i554.transparency = i555[18]
  i554.bounds = i555[19]
  i554.pixelsPerUnit = i555[20]
  i554.textureWidth = i555[21]
  i554.textureHeight = i555[22]
  i554.nativeSize = new pc.Vec2( i555[23], i555[24] )
  i554.pivot = new pc.Vec2( i555[25], i555[26] )
  i554.textureRectOffset = new pc.Vec2( i555[27], i555[28] )
  return i554
}

Deserializers["Project.Scripts.CameraManagement.CameraSettings"] = function (request, data, root) {
  var i556 = root || request.c( 'Project.Scripts.CameraManagement.CameraSettings' )
  var i557 = data
  i556.Offset = new pc.Vec3( i557[0], i557[1], i557[2] )
  i556.LerpSpeed = i557[3]
  return i556
}

Deserializers["Project.Scripts.LevelManagement.Settings.LevelSettings"] = function (request, data, root) {
  var i558 = root || request.c( 'Project.Scripts.LevelManagement.Settings.LevelSettings' )
  var i559 = data
  i558.Height = i559[0]
  i558.Width = i559[1]
  i558.BlankSpace = i559[2]
  i558.TileSize = i559[3]
  i558.Offset = new pc.Vec2( i559[4], i559[5] )
  i558.Seed = i559[6]
  i558.RandomSeed = !!i559[7]
  request.r(i559[8], i559[9], 0, i558, 'GroundSettings')
  request.r(i559[10], i559[11], 0, i558, 'FenceSettings')
  return i558
}

Deserializers["Project.Scripts.LevelManagement.Settings.GroundSettings"] = function (request, data, root) {
  var i560 = root || request.c( 'Project.Scripts.LevelManagement.Settings.GroundSettings' )
  var i561 = data
  var i563 = i561[0]
  var i562 = []
  for(var i = 0; i < i563.length; i += 1) {
    i562.push( request.d('Project.Scripts.LevelManagement.GroundTile', i563[i + 0]) );
  }
  i560.Tiles = i562
  return i560
}

Deserializers["Project.Scripts.LevelManagement.GroundTile"] = function (request, data, root) {
  var i566 = root || request.c( 'Project.Scripts.LevelManagement.GroundTile' )
  var i567 = data
  i566.Weight = i567[0]
  request.r(i567[1], i567[2], 0, i566, 'TilePrefab')
  return i566
}

Deserializers["Project.Scripts.LevelManagement.Settings.FenceSettings"] = function (request, data, root) {
  var i568 = root || request.c( 'Project.Scripts.LevelManagement.Settings.FenceSettings' )
  var i569 = data
  request.r(i569[0], i569[1], 0, i568, 'FencePostPrefab')
  request.r(i569[2], i569[3], 0, i568, 'HorizontalConnectionPrefab')
  request.r(i569[4], i569[5], 0, i568, 'VerticalConnectionPrefab')
  return i568
}

Deserializers["Project.Scripts.Entity.Player.Movement.MovementSettings"] = function (request, data, root) {
  var i570 = root || request.c( 'Project.Scripts.Entity.Player.Movement.MovementSettings' )
  var i571 = data
  i570.TranslationSpeed = i571[0]
  i570.Acceleration = i571[1]
  i570.Deceleration = i571[2]
  return i570
}

Deserializers["Project.Scripts.Entity.Player.Combat.CombatSettings"] = function (request, data, root) {
  var i572 = root || request.c( 'Project.Scripts.Entity.Player.Combat.CombatSettings' )
  var i573 = data
  request.r(i573[0], i573[1], 0, i572, 'WeaponPrefab')
  i572.StartWeaponCount = i573[2]
  i572.MaxWeaponCount = i573[3]
  i572.CycleSpeed = i573[4]
  i572.ArrangeTranslationSpeed = i573[5]
  i572.ArrangeRotationSpeed = i573[6]
  i572.CenterDistance = i573[7]
  return i572
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i574 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i575 = data
  var i577 = i575[0]
  var i576 = []
  for(var i = 0; i < i577.length; i += 1) {
    i576.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i577[i + 0]) );
  }
  i574.files = i576
  i574.componentToPrefabIds = i575[1]
  return i574
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i580 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i581 = data
  i580.path = i581[0]
  request.r(i581[1], i581[2], 0, i580, 'unityObject')
  return i580
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i582 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i583 = data
  var i585 = i583[0]
  var i584 = []
  for(var i = 0; i < i585.length; i += 1) {
    i584.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i585[i + 0]) );
  }
  i582.scriptsExecutionOrder = i584
  var i587 = i583[1]
  var i586 = []
  for(var i = 0; i < i587.length; i += 1) {
    i586.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i587[i + 0]) );
  }
  i582.sortingLayers = i586
  var i589 = i583[2]
  var i588 = []
  for(var i = 0; i < i589.length; i += 1) {
    i588.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i589[i + 0]) );
  }
  i582.cullingLayers = i588
  i582.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i583[3], i582.timeSettings)
  i582.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i583[4], i582.physicsSettings)
  i582.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i583[5], i582.physics2DSettings)
  i582.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i583[6], i582.qualitySettings)
  i582.enableRealtimeShadows = !!i583[7]
  i582.enableAutoInstancing = !!i583[8]
  i582.enableStaticBatching = !!i583[9]
  i582.enableDynamicBatching = !!i583[10]
  i582.lightmapEncodingQuality = i583[11]
  i582.desiredColorSpace = i583[12]
  var i591 = i583[13]
  var i590 = []
  for(var i = 0; i < i591.length; i += 1) {
    i590.push( i591[i + 0] );
  }
  i582.allTags = i590
  return i582
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i594 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i595 = data
  i594.name = i595[0]
  i594.value = i595[1]
  return i594
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i598 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i599 = data
  i598.id = i599[0]
  i598.name = i599[1]
  i598.value = i599[2]
  return i598
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i602 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i603 = data
  i602.id = i603[0]
  i602.name = i603[1]
  return i602
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i604 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i605 = data
  i604.fixedDeltaTime = i605[0]
  i604.maximumDeltaTime = i605[1]
  i604.timeScale = i605[2]
  i604.maximumParticleTimestep = i605[3]
  return i604
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i606 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i607 = data
  i606.gravity = new pc.Vec3( i607[0], i607[1], i607[2] )
  i606.defaultSolverIterations = i607[3]
  i606.bounceThreshold = i607[4]
  i606.autoSyncTransforms = !!i607[5]
  i606.autoSimulation = !!i607[6]
  var i609 = i607[7]
  var i608 = []
  for(var i = 0; i < i609.length; i += 1) {
    i608.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i609[i + 0]) );
  }
  i606.collisionMatrix = i608
  return i606
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i612 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i613 = data
  i612.enabled = !!i613[0]
  i612.layerId = i613[1]
  i612.otherLayerId = i613[2]
  return i612
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i614 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i615 = data
  request.r(i615[0], i615[1], 0, i614, 'material')
  i614.gravity = new pc.Vec2( i615[2], i615[3] )
  i614.positionIterations = i615[4]
  i614.velocityIterations = i615[5]
  i614.velocityThreshold = i615[6]
  i614.maxLinearCorrection = i615[7]
  i614.maxAngularCorrection = i615[8]
  i614.maxTranslationSpeed = i615[9]
  i614.maxRotationSpeed = i615[10]
  i614.baumgarteScale = i615[11]
  i614.baumgarteTOIScale = i615[12]
  i614.timeToSleep = i615[13]
  i614.linearSleepTolerance = i615[14]
  i614.angularSleepTolerance = i615[15]
  i614.defaultContactOffset = i615[16]
  i614.autoSimulation = !!i615[17]
  i614.queriesHitTriggers = !!i615[18]
  i614.queriesStartInColliders = !!i615[19]
  i614.callbacksOnDisable = !!i615[20]
  i614.reuseCollisionCallbacks = !!i615[21]
  i614.autoSyncTransforms = !!i615[22]
  var i617 = i615[23]
  var i616 = []
  for(var i = 0; i < i617.length; i += 1) {
    i616.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i617[i + 0]) );
  }
  i614.collisionMatrix = i616
  return i614
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i621 = data
  i620.enabled = !!i621[0]
  i620.layerId = i621[1]
  i620.otherLayerId = i621[2]
  return i620
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i622 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i623 = data
  var i625 = i623[0]
  var i624 = []
  for(var i = 0; i < i625.length; i += 1) {
    i624.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i625[i + 0]) );
  }
  i622.qualityLevels = i624
  var i627 = i623[1]
  var i626 = []
  for(var i = 0; i < i627.length; i += 1) {
    i626.push( i627[i + 0] );
  }
  i622.names = i626
  i622.shadows = i623[2]
  i622.anisotropicFiltering = i623[3]
  i622.antiAliasing = i623[4]
  i622.lodBias = i623[5]
  i622.shadowCascades = i623[6]
  i622.shadowDistance = i623[7]
  i622.shadowmaskMode = i623[8]
  i622.shadowProjection = i623[9]
  i622.shadowResolution = i623[10]
  i622.softParticles = !!i623[11]
  i622.softVegetation = !!i623[12]
  i622.activeColorSpace = i623[13]
  i622.desiredColorSpace = i623[14]
  i622.masterTextureLimit = i623[15]
  i622.maxQueuedFrames = i623[16]
  i622.particleRaycastBudget = i623[17]
  i622.pixelLightCount = i623[18]
  i622.realtimeReflectionProbes = !!i623[19]
  i622.shadowCascade2Split = i623[20]
  i622.shadowCascade4Split = new pc.Vec3( i623[21], i623[22], i623[23] )
  i622.streamingMipmapsActive = !!i623[24]
  i622.vSyncCount = i623[25]
  i622.asyncUploadBufferSize = i623[26]
  i622.asyncUploadTimeSlice = i623[27]
  i622.billboardsFaceCameraPosition = !!i623[28]
  i622.shadowNearPlaneOffset = i623[29]
  i622.streamingMipmapsMemoryBudget = i623[30]
  i622.maximumLODLevel = i623[31]
  i622.streamingMipmapsAddAllCameras = !!i623[32]
  i622.streamingMipmapsMaxLevelReduction = i623[33]
  i622.streamingMipmapsRenderersPerFrame = i623[34]
  i622.resolutionScalingFixedDPIFactor = i623[35]
  i622.streamingMipmapsMaxFileIORequests = i623[36]
  i622.currentQualityLevel = i623[37]
  return i622
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i630 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i631 = data
  request.r(i631[0], i631[1], 0, i630, 'm_ObjectArgument')
  i630.m_ObjectArgumentAssemblyTypeName = i631[2]
  i630.m_IntArgument = i631[3]
  i630.m_FloatArgument = i631[4]
  i630.m_StringArgument = i631[5]
  i630.m_BoolArgument = !!i631[6]
  return i630
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"enabled":19},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.SpriteRenderer":{"color":0,"sprite":4,"flipX":6,"flipY":7,"drawMode":8,"size":9,"tileMode":11,"adaptiveModeThreshold":12,"maskInteraction":13,"spriteSortPoint":14,"enabled":15,"sharedMaterial":16,"sharedMaterials":18,"receiveShadows":19,"shadowCastingMode":20,"sortingLayerID":21,"sortingOrder":22,"lightmapIndex":23,"lightmapSceneIndex":24,"lightmapScaleOffset":25,"lightProbeUsage":29,"reflectionProbeUsage":30},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Components.BoxCollider2D":{"usedByComposite":0,"autoTiling":1,"size":2,"edgeRadius":4,"enabled":5,"isTrigger":6,"usedByEffector":7,"density":8,"offset":9,"material":11},"Luna.Unity.DTO.UnityEngine.Components.RectTransform":{"pivot":0,"anchorMin":2,"anchorMax":4,"sizeDelta":6,"anchoredPosition3D":8,"rotation":11,"scale":15},"Luna.Unity.DTO.UnityEngine.Components.Canvas":{"planeDistance":0,"referencePixelsPerUnit":1,"isFallbackOverlay":2,"renderMode":3,"renderOrder":4,"sortingLayerName":5,"sortingOrder":6,"scaleFactor":7,"worldCamera":8,"overrideSorting":10,"pixelPerfect":11,"targetDisplay":12,"overridePixelPerfect":13,"enabled":14},"Luna.Unity.DTO.UnityEngine.Components.CanvasRenderer":{"cullTransparentMesh":0},"Luna.Unity.DTO.UnityEngine.Components.Rigidbody2D":{"bodyType":0,"material":1,"simulated":3,"useAutoMass":4,"mass":5,"drag":6,"angularDrag":7,"gravityScale":8,"collisionDetectionMode":9,"sleepMode":10,"constraints":11},"Luna.Unity.DTO.UnityEngine.Components.PolygonCollider2D":{"usedByComposite":0,"autoTiling":1,"points":2,"enabled":3,"isTrigger":4,"usedByEffector":5,"density":6,"offset":7,"material":9},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"isCreatedByShaderGraph":10,"disableBatching":11,"compiled":12},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Textures.Sprite":{"name":0,"texture":1,"aabb":3,"vertices":4,"triangles":5,"textureRect":6,"packedRect":10,"border":14,"transparency":18,"bounds":19,"pixelsPerUnit":20,"textureWidth":21,"textureHeight":22,"nativeSize":23,"pivot":25,"textureRectOffset":27},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37}}

Deserializers.requiredComponents = {"43":[44],"45":[44],"46":[44],"47":[44],"48":[44],"49":[44],"50":[51],"52":[2],"53":[54],"55":[54],"56":[54],"57":[54],"58":[54],"59":[54],"60":[54],"61":[27],"62":[27],"63":[27],"64":[27],"65":[27],"66":[27],"67":[27],"68":[27],"69":[27],"70":[27],"71":[27],"72":[27],"73":[27],"74":[2],"75":[76],"77":[78],"79":[78],"20":[19],"80":[23],"81":[82],"83":[19],"84":[76,19],"85":[19,24],"86":[19],"87":[24,19],"88":[76],"89":[24,19],"90":[19],"91":[19],"92":[19],"23":[20],"25":[24,19],"93":[19],"22":[20],"94":[19],"95":[19],"96":[19],"97":[19],"98":[19],"99":[19],"100":[19],"101":[19],"102":[19],"103":[24,19],"104":[19],"105":[19],"106":[19],"107":[19],"108":[24,19],"109":[19],"110":[36],"111":[36],"37":[36],"112":[36],"113":[2],"114":[2],"115":[2],"116":[117]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.Light","UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.MonoBehaviour","Project.Scripts.CameraManagement.CameraManager","Project.Scripts.CameraManagement.CameraSettings","Cinemachine.CinemachineBrain","Cinemachine.CinemachineVirtualCamera","Cinemachine.CinemachinePipeline","Cinemachine.CinemachineFramingTransposer","Project.Scripts.GameManager","Project.Scripts.LevelManagement.LevelManager","Project.Scripts.LevelManagement.Settings.LevelSettings","UnityEngine.SpriteRenderer","UnityEngine.Sprite","UnityEngine.Material","UnityEngine.Shader","UnityEngine.BoxCollider2D","UnityEngine.RectTransform","UnityEngine.Canvas","UnityEngine.EventSystems.UIBehaviour","UnityEngine.UI.CanvasScaler","UnityEngine.UI.GraphicRaycaster","UnityEngine.CanvasRenderer","UnityEngine.UI.Image","DynamicJoystick","UnityEngine.Rigidbody2D","Project.Scripts.Entity.Player.PlayerEntity","Project.Scripts.Entity.Player.Movement.MovementSystem","Project.Scripts.Entity.Player.Movement.MovementSettings","Project.Scripts.Entity.Player.Combat.CombatSystem","Project.Scripts.Entity.Player.Combat.CombatSettings","Project.Scripts.CollisionManagement.CollisionBroadcaster2D","UnityEngine.PolygonCollider2D","Project.Scripts.Entity.Sword.SwordEntity","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.Cubemap","UnityEngine.Texture2D","Project.Scripts.LevelManagement.Settings.GroundSettings","Project.Scripts.LevelManagement.Settings.FenceSettings","UnityEngine.GameObject","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.MeshRenderer","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","ScratchCardAsset.Core.InputData.CanvasGraphicRaycaster","UnityEngine.U2D.SpriteShapeController","UnityEngine.U2D.SpriteShapeRenderer","TMPro.TextContainer","TMPro.TextMeshPro","TMPro.TextMeshProUGUI","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Text","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Cinemachine.CinemachineExternalCamera","Cinemachine.GroupWeightManipulator","Cinemachine.CinemachineTargetGroup"]

Deserializers.unityVersion = "2022.3.62f1";

Deserializers.productName = "LoopGamesCaseStudy";

Deserializers.lunaInitializationTime = "02/04/2026 01:06:38";

Deserializers.lunaDaysRunning = "0.0";

Deserializers.lunaVersion = "6.4.0";

Deserializers.lunaSHA = "6639120529aa36186c6141b5c3fb20246c28bff0";

Deserializers.creativeName = "";

Deserializers.lunaAppID = "36531";

Deserializers.projectId = "1f1a6f207d324da40a08ca88dc77c027";

Deserializers.packagesInfo = "com.unity.cinemachine: 2.10.5\ncom.unity.textmeshpro: 3.0.9\ncom.unity.timeline: 1.7.7\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "False";

Deserializers.runtimeAnalysisExcludedClassesCount = "0";

Deserializers.runtimeAnalysisExcludedMethodsCount = "0";

Deserializers.runtimeAnalysisExcludedModules = "";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "False";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "StandaloneWindows64";

Deserializers.applicationIdentifier = "com.DefaultCompany.LoopGamesCaseStudy";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 28;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "b32d0ec2-c4be-49c9-bad2-2c22cbabcf34";

Deserializers.runtimeInitializeOnLoadInfos = [[["Cinemachine","CinemachineCore","InitializeModule"],["Cinemachine","CinemachineStoryboard","InitializeModule"],["Cinemachine","CinemachineImpulseManager","InitializeModule"],["Cinemachine","UpdateTracker","InitializeModule"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[],[["$BurstDirectCallInitializer","Initialize"],["$BurstDirectCallInitializer","Initialize"]],[],[]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

