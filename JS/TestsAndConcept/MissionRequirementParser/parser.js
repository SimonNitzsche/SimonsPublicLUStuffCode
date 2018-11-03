class Parser {
    constructor() {
        this.inputIndex = 0;
        this.openBrackets = 0;
        this.openBracketsLimit = 12;
        console.clear();
    }
    hasMissionCompleted(completedMissions, mission) {
        return completedMissions.includes(mission);
    }
    combine(oldV, newV, mode) {
        console.log("combine ["+oldV+"] "+mode+" ["+newV+"]");
        switch(mode) {
            case "OR": {
                return !!(!!oldV | !!newV);
            }
            case "AND": {
                return !!(!!oldV & !!newV);
            }
            default: {
                throw ("Unknown operation: "+mode);
            }
        }
    }
    parse(completedMissions, input) {
        let result = false;
        let buffer = "";
        let mode = "OR";
        console.log(input);

        if(this.openBrackets > this.openBracketsLimit) {
            throw "Too many open brackets.";
        }

        for(; this.inputIndex < input.length; ++this.inputIndex) {
            switch(input[this.inputIndex]) {
                case " ": break;
                case ",":
                case "&": {
                    result = this.combine(result, this.hasMissionCompleted(completedMissions, buffer), mode);
                    if(!result) {
                        // 0 and 1 is already false, so we can kill it here.
                        return result;
                    }
                    mode = "AND";
                    buffer = "";
                    break;
                }
                case "|": {
                    result = this.combine(result, this.hasMissionCompleted(completedMissions, buffer), mode);
                    mode = "OR";
                    buffer = "";
                    break;
                }
                case "(":  {
                    this.openBrackets++;
                    this.inputIndex++;
                    result = this.combine(result, this.parse(completedMissions, input), mode);
                    break;
                }
                case ")": {
                    if(this.openBrackets>0) {
                        // We now know we are in an inner loop for MRP
                        return this.combine(result, this.hasMissionCompleted(completedMissions, buffer), mode);
                    }
                    (this.openBrackets<=0) ? (this.openBrackets=0) : this.openBrackets--;
                    break;
                }
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case ":": {
                    buffer += input[this.inputIndex];
                    break;
                }
                default: {
                    throw "Unknown operator: "+input[this.inputIndex];
                }
            }
            console.log(buffer);
        }
        result = this.combine(result, this.hasMissionCompleted(completedMissions, buffer), mode);
        return result;
    }
}
