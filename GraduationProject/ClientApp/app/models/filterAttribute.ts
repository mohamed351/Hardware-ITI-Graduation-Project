export class filterAttribute{
    TitleName: string;
    FilterType: string;
    QueryKey: string;
    Choices: string[];
}

export class filter{
    TitleName: string[]; 
    FilterType: string[];
    choices: dictionary[];
}

export class dictionary{
    choice: string;
    value: any[];
}