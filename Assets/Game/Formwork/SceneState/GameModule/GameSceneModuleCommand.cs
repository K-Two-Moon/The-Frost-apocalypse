using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令模块
/// 负责接收命令并执行
/// </summary>
public class GameSceneModuleCommand : IModule
{
    // 命令执行队列
    private Queue<ICommand> commandQueue = new Queue<ICommand>();
    // 命令历史记录(用于撤销)
    private Stack<ICommand> commandHistory = new Stack<ICommand>();

    public GameSceneModuleCommand(GameState gameState) : base(gameState)
    {
    }

    /// <summary>
    /// 添加命令到队列
    /// </summary>
    public void AddCommand(ICommand command)
    {
        commandQueue.Enqueue(command);
    }

    /// <summary>
    /// 撤销上一条命令
    /// </summary>
    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            var command = commandHistory.Pop();
            // 假设命令实现了IUndoable接口
            if (command is IUndoable undoable)
            {
                undoable.Undo();
            }
        }
    }

    /// <summary>
    /// 更新模块
    /// 从命令队列中取出命令并执行
    /// </summary>
    public override void Update()
    {
        while (commandQueue.Count > 0)
        {
            var command = commandQueue.Dequeue();
            command.Execute();
            if (command is IUndoable undoable)
                commandHistory.Push(command);
        }
    }

    // 输入相关命令
    public class MoveCommand : ICommand
    {
        private Vector2 moveInput;
        public MoveCommand(Vector2 moveInput) => this.moveInput = moveInput;
        public void Execute() => Debug.Log($"Move command executed: {moveInput}");
    }

    public class JumpCommand : ICommand
    {
        public void Execute() => Debug.Log("Jump command executed");
    }

    public class MenuCommand : ICommand
    {
        public void Execute() => Debug.Log("Menu command executed");
    }

    public class MouseClickCommand : ICommand
    {
        private int button;
        public MouseClickCommand(int button) => this.button = button;
        public void Execute() => Debug.Log($"Mouse click command executed: button {button}");
    }
}
