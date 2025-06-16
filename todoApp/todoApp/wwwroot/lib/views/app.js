var app = app || {};
app.Todo = Backbone.Model.extend({
    defaults: {
        title: '',
        done: false,
        order: 0
    },
    toggle: function () {
        this.save({ done: !this.get('done') });
    }
});
app.TodoList = Backbone.Collection.extend({
    model: app.Todo,
    localStorage: new Backbone.LocalStorage('todos-backbone'),
    nextOrder: function () {
        return this.length ? this.last().get('order') + 1 : 1;
    },
    comparator: 'order'
});

app.todos = new app.TodoList();
app.TodoView = Backbone.View.extend({
    tagName: 'li',
    template: _.template(`
    <div class="view">
        <input type="checkbox" <% if(done) { %>checked<% } %>> 
        <label><%- title %></label> 
        <button class="delete">X</button>
    </div>
    <input class="edit" type="text" style="display:none;">
    `),
    events: {
        'change input[type="checkbox"]': 'toggleDone',
        'click .delete': 'deleteTodo',
        'click #clear-completed': 'clearCompleted',
        'dblclick label': 'editTodo',
        'keypress .edit': 'updateOnEnter',
        'blur .edit': 'closeEdit'
    },
    initialize: function () {
        this.listenTo(this.model, 'change', this.render);
        this.listenTo(this.model, 'destroy', this.remove);
    },
    render: function () {
        this.$el.html(this.template(this.model.toJSON()));
        return this;
        const remaining = app.todos.where({ done: false }).length;
        this.count.text(`${remaining} việc chưa hoàn thành`);
        const completed = app.todos.where({ done: true }).length;
        this.clearButton.toggle(completed > 0);
    },
    editTodo: function () {
        this.$el.addClass('editing');
        this.$('.edit').val(this.model.get('title'));
        this.$('.view').hide();
        this.$('.edit').show().focus();
    },
    closeEdit: function () {
        const value = this.$('.edit').val().trim();
        if (value) {
            this.model.save({ title:value });
            this.$el.removeClass('editing');
            this.$('.edit').hide();
            this.$('.view').show();
        }
    },
    updateOnEnter: function (e) {
        if (e.which === 13) {
            this.closeEdit();
        }
    },
    toggleDone: function () {
        this.model.toggle();
    },
    deleteTodo: function () {
        this.model.destroy();
    },
    clearCompleted: function () {
        _.invoke(app.todos.where({ done: true }), 'destroy');
    }
});
app.AppView = Backbone.View.extend({
    el: '#todoApp',
    events: {
        'keypress #new-todo': 'createOnEnter',
        'click #clear-completed': 'clearCompleted'
    },
    initialize: function () {
        this.input = this.$('#new-todo');
        this.list = this.$('#todo-list');
        //this.footer = this.$('footer');
        
        this.clearButton = this.$('#clear-completed');
        this.listenTo(app.todos, 'add', this.addOne);
        this.listenTo(app.todos, 'reset', this.addAll);
        this.listenTo(app.todos, 'all', this.updateCount);
        app.todos.fetch();
    },
    clearCompleted: function () {
        _.invoke(app.todos.where({ done: true }), 'destroy');
    },
    createOnEnter: function (e) {
        if ((e.keyCode || e.which) === 13 && this.input.val().trim()) {
            app.todos.create({
                title: this.input.val().trim(),
                done: false,
                order: app.todos.nextOrder()
            });
            this.input.val('');
        }
    },
    updateCount: function () {
        var remaining = app.todos.where({ done: false }).length;
        $('#todo-count').text(remaining + ' việc chưa hoàn thành');
        var hasCompleted = app.todos.where({ done: true }).length > 0;
        if (hasCompleted) {
            $('#clear-completed').show();
        } else {
            $('#clear-completed').hide();
        }
    },
    addOne: function (todo) {
        var view = new app.TodoView({ model: todo });
        this.list.append(view.render().el);
    },
    addAll: function () {
        this.list.empty();
        app.todos.each(this.addOne, this);
    }
});
$(function () {
    new app.AppView();
});