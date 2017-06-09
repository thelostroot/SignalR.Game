function Game() {
    this.Units = [];
    this.IsEmpty = true;
    this.draw = null;
    this.ClienAnimationIntreval = null;
}

Game.prototype.Init = function (settings) {

    this.draw = SVG('game').size(settings.AreaWidth, settings.AreaHeight);
    this.ClienAnimationIntreval = settings.ClienAnimationIntreval;

    for (var i = 0; i < settings.MaxUnitsCount; i++) {
        var image = this.draw.image("", 130, 145);
        image.click(this.ClickHandler);

        this.Units[i] = {
            id: null,
            svgImage: image
        };
    }
};

Game.prototype.ClickHandler = function (e) {
    // hack
    Game.prototype.KillPerson(e.target);
};


Game.prototype.UpdateArea = function(data) {
    if (this.IsEmpty)
        this.FirstUnitFill(data);

    if (data.length === 0)
        return;
    
    var newUnits = [];
    for (var i = 0; i < data.length; i++) {
        var unit = this.GetUnitById(data[i].Id);
        if (unit != null) {
            var animationStepDuration = Math.round(this.ClienAnimationIntreval / data[i].MovePath.length);
            for (var j = 0; j < data[i].MovePath.length; j++) {
                unit.svgImage.animate(animationStepDuration).move(data[i].MovePath[j].X, data[i].MovePath[j].Y);
            }
        }
        else
            newUnits.push(data[i]);
    }

    for (var k = 0; k < newUnits.length; k++) {
        var emptyUnit = this.GetEmptyUnit();
        var animationStepDuration = Math.round(this.ClienAnimationIntreval / data[k].MovePath.length);
        for (var pointIndex = 0; pointIndex < data[k].MovePath.length; pointIndex++) {
            emptyUnit.svgImage.animate(animationStepDuration).move(data[k].MovePath[pointIndex].X, data[k].MovePath[pointIndex].Y);
        }
    }
};

Game.prototype.GetUnitById = function(id) {
    for (var i = 0; i < this.Units.length; i++) {
        if (this.Units[i].id == id)
            return this.Units[i];
    }
    return null;
}

Game.prototype.GetEmptyUnit = function () {
    for (var i = 0; i < this.Units.length; i++) {
        if (this.Units[i] === null)
            return this.Units[i];
    }
    return null;
}

Game.prototype.FirstUnitFill = function(data) {
    for (var i = 0; i < data.length; i++) {
        this.Units[i].id = data[i].Id;

        this.Units[i].svgImage.load(MOBS[data[i].GamePerson]);
        this.Units[i].svgImage.data("id", data[i].Id);

        var animationStepDuration = Math.round(this.ClienAnimationIntreval / data[i].MovePath.length);
        for (var j = 0; j < data[i].MovePath.length; j++) {
            this.Units[i].svgImage.animate(animationStepDuration).move(data[i].MovePath[j].X, data[i].MovePath[j].Y);
        }
        if (data.MoveAnimation != 0) { };
    }
    this.IsEmpty = false;
}

Game.prototype.UpdateKillUnit = function(id) {
    
    for (var i = 0; i < this.Units.length; i++) {
        if (this.Units[i].id == id) {
            this.Units[i].id = null;
            this.Units[i].svgImage.hide();
        }
    }
};

Game.prototype.KillPerson = function(el) {
    var unitId;
    if (el.dataset == undefined)
        unitId = el.getAttribute("data-id");
    else
        unitId = el.dataset.id;

    GameHub.server.killUnit(unitId);
};