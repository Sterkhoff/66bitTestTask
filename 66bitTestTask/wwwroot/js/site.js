var activeFormPlayerId = null;
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/Updater")
    .build();

hubConnection.on("Receive", function () {
    if (document.getElementById("players-list") == null) {
        return
    }
    else {
        $.ajax({
            url: '/Home/ShowSoccerPlayers',
            method: 'GET',
            dataType: 'html',
            success: (result) => {
                document.getElementById("players-list").innerHTML = $(result).find('#players-list')[0].innerHTML;
            },
            error: (error) => {
                console.log(error);
            }
        })
    }
});

hubConnection.start()
    .catch(function (err) {
        return console.error(err.toString());
    });

function toggleTeamNameButton(id) {
    var teamNameSelect = document.getElementById("team-name-" + id);
    var teamNameInput = document.getElementById("team-name-input-" + id);
    var teamNameButton = document.getElementById("team-name-button-" + id);
    if (teamNameButton.textContent == "Выбрать команду") {
        teamNameSelect.style.display = "block";
        teamNameSelect.removeAttribute("disabled");
        teamNameInput.style.display = "none";
        teamNameInput.value = "";
        teamNameInput.setAttribute("disabled", true)
        teamNameButton.textContent = "Добавить свою команду";
    }
    else {
        teamNameSelect.style.display = "none";
        teamNameSelect.setAttribute("disabled", true);
        teamNameInput.removeAttribute("disabled");
        teamNameInput.style.display = "block";
        teamNameButton.textContent = "Выбрать команду";
    }
}

function confirmButtonScript(id) {
    var teamIsNewContainer = document.getElementById("team-is-new-" + id);
    var teamNameInput = document.getElementById("team-name-input-" + id);
    if (teamNameInput.value != "") {
        teamIsNewContainer.value = "true";
    }
    else {
        teamIsNewContainer.value = "false";
    }
}

function toggleForm(id) {
    if (activeFormPlayerId != null) {
        resetChanges(activeFormPlayerId);
    }
    activeFormPlayerId = id;
    form = document.getElementById("change-player-" + id + "-form");
    data = document.getElementById("player-" + id + "-data");
    form.style.display = "block";
    data.style.display = "none";
}

function resetChanges(id) {
    form = document.getElementById("change-player-" + id + "-form");
    data = document.getElementById("player-" + id + "-data");
    form.style.display = "none";
    data.style.display = "block";
}