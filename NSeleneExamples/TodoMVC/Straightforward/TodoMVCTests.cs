﻿using NUnit.Framework;
using static NSelene.Selene;
using NSelene;
using OpenQA.Selenium;
using NSelene.Support.Extensions;

namespace NSeleneExamples.TodoMVC.StraightForward
{
    [TestFixture]
    public class TestTodoMVC : BaseTest
    {
        [Test]
        public void FilterTasks()
        {
            Open("https://todomvc4tasj.herokuapp.com/");

            WaitTo(Have.JSReturnedTrue (
                "return " +
                "$._data($('#new-todo').get(0), 'events').hasOwnProperty('keyup')&& " +
                "$._data($('#toggle-all').get(0), 'events').hasOwnProperty('change') && " +
                "$._data($('#clear-completed').get(0), 'events').hasOwnProperty('click')"));

            S("#new-todo").SetValue("a").PressEnter();
            S("#new-todo").SetValue("b").PressEnter();
            S("#new-todo").SetValue("c").PressEnter();
            SS("#todo-list>li").Should(Have.ExactTexts("a", "b", "c"));

            SS("#todo-list>li").FindBy(Have.ExactText("b")).Find(".toggle").Click();

            S(By.LinkText("Active")).Click();
            SS("#todo-list>li").FilterBy(Be.Visible).Should(Have.ExactTexts("a", "c"));

            S(By.LinkText("Completed")).Click();
            SS("#todo-list>li").FilterBy(Be.Visible).Should(Have.ExactTexts("b"));
        }
    }
}
