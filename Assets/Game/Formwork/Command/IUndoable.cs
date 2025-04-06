using System;

/// <summary>
/// 可撤销命令接口
/// </summary>
public interface IUndoable : ICommand
{
    /// <summary>
    /// 撤销命令执行
    /// </summary>
    void Undo();
}
