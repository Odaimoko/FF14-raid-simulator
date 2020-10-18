# FF14-raid-Simulator

制作总结。

**冰圈特效**：不想画圆，直接用图片素材。

​	prefab 里的：地面 aoe，空中 3 白圈，地面 aoe碰撞圆。地面 aoe 和白圈是图片，碰撞圆是球，压缩y 到0.01，并且用 mesh collider。记住，要collision或者 trigger enter事件必须要在碰撞体上加入刚体和 collider 两个 component。控制脚本是IceRingEffect.cs。

​	代码里生成的：旋转的冰锥。冰锥本身是 prefab。控制公转的脚本是 Orbit.cs。

## 玩家

模型：免费商店素材Meshtint Free Chicken。

**玩家朝相机正方向移动**。

​	获取相机正方向：`GetCamForward`，用 transform 的 forward 属性，除去 Y 轴的坐标，然后 normalize。

​	获取输入：Input.GetAxis。

​	Challenge：输入的是标准坐标系的上下左右向量。相机旋转后，要以相机正方向重新计算运动方向的向量。

​	解决：如果不做任何处理，直接用 GetAxis 得到的方向（图中的 in）进行移动，那么相机无论怎么转，都是按照世界坐标系的 in 移动。由于相机旋转了（图中绿色角），我们在世界坐标系里的移动也需要旋转。

​	旋转角度的获取：`Vector3.Angle(Vector3.forward, camForward) / 180 * Mathf.PI`。Angle 方法获取的角度只有 0~180，所以如果 cam 方向的 X>0，要用 2pi 减去得到的结果。

​	旋转 input 向量：使用旋转矩阵，手算。注意旋转矩阵是逆时针的。

<img src="assets/image-20200919212644099.png" alt="image-20200919212644099" style="zoom:50%;" />

**减小输入惯性**：将Input Manager 输入 axis 的 gravity设为比较高的值。

**玩家逐渐转向至前进方向**。使用欧拉旋转角度（忘了四元数）。**逐渐**转向的逐渐由微积分思路得来，转动=`\int_startAngle^endAngle spinSpeed dx`。

​	旋转目标方向：上面 in向量旋转后的结果，称之为`towards`。

​	原旋转角度： `rot_y=transform.eulerAngles.y`。

​	微小旋转量：`\delta = towards-rot_y`。

​	旋转：`tranform.Rotate`。

​	盲点：首先，要将`towards`向量转为 y 轴的旋转角度，同样用 V3.Angle。

​	其次，如果现在朝向前方，想要转到左方，`towards`就是 270，`rot_y=0`。`tranform.Rotate`是顺时针，于是这样就会从右半边转到左边，但是更好的是左转 90 度。于是就要写判断，从更小的角度的那一边转过去。

### 控制

TODO

### Entity：Player 和 Enemy

TODO



## 战斗

BattleManager：用于处理每场战斗都会有的东西，例如 buff 结算、胜负判定。

Scenario：每场战斗不一样的地方，也就是不同的 boss 会有不同的效果。

Animation：时间轴，使用Animator Controller 的状态机实现转阶段（P1/P2……）的效果。使用Animation Event 实现时间轴。

### Status：buff 和 debuff 的实现

用事件的思路实现的。BM：BattleManager 的缩写。

如果要使一个效果生效，首先在 BM 里注册效果的处理队列。BM 在每帧的 Update 里会使已注册的效果生效（调用`SingleStatus`的`Apply`），每三秒注册一次 scene 里每个 Enemy 和 Player的`NormalEffect`。

效果的实现是两层。最底层是`SingleStatus`类，用一个`StatusGroup`封装。`StatusGroup`是考虑到有些效果会共通作用，比如绝亚P2最终审判，可以让每个人身上的 buff 都放在这个 group 里，最后生效的时候调用`StatusGroup`的生效函数。但是其实也可以通过检测其他 entity 身上的 buff 情况实现，待看。现在为止它只是在单纯包着一个`SingleStatus`。

`SingleStatus`初始化的时候会要求传入`from`和`target`。如果想要实现不同的效果，重写`SingleStatus`的`NormalEffect`和`ExpireEffect`，分别是每三秒持续的效果和倒计时结束的效果。同时还有个`OnAttachedToEntity`，是一附到玩家/敌人身上立马就运行的函数。默认是一上身就生效，可以通过设置`effectiveAtOnce=false`修改。每一个`SingleStatus`有一个ID，游戏全局每多一个`SingleStatus`实例，就会自增1。

这样建模可以将几乎所有的状态全部囊括，例如自动攻击，就可以建立一个倒计时为 1 秒的状态，倒计时结束后对对象造成伤害。

**事件链**：

每三秒：BM 调用所有 `Entity` 的 `RegisterEffect`方法—— `Entity` 调用`StatusGroup`——`StatusGroup`调用`SingleStatus`——`SingleStatus`调用 BM 的 `AddEvent` 方法将自己的效果加入处理队列。

每帧：BM 使处理队列里的效果生效。`SingleStatus`的`Apply`会调用`NormalEffect`。

每新有 Status 注册到 Entity 上：运行`OnAttachedToEntity`。

每有 Status 倒计时结束：注册一次，`SingleStatus`的`Apply`此时会调用`ExpireEffect`。

## UI

UI Manager：绘制 UI。 主要是获取场景里敌人和玩家的信息。

### 小队列表



### 自己的状态

用字典存储已经在显示在屏幕上的状态集合（用来显示图标的GameObject，对应的`SingleStatus`实例，以及是否显示的flag不过暂时没用）。Key是`SingleStatus`实例的名字+ID

每帧检查：状态如果到期了，从字典里去掉，并且Destroy。

- [x] TODO：可以优化为可重复使用的Object Pool而不是每次都Instantiate+Destroy。

<img src="D:\D\unity prj\Lesson5\Assets\assets\image-20201009185651827.png" alt="image-20201009185651827" style="zoom:50%;" />

