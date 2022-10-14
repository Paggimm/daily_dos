import {RouterDefinitions} from "@/enums/RouterDefinitions";

export interface IHomeActionCardConfiguration {
    title: string,
    routerLink: RouterDefinitions,
}

// builds an array containing actionCard-Properties
export function buildHomeActionCardPropList(): IHomeActionCardConfiguration[] {
    return [
        { title: 'ACTIVITIES', routerLink: RouterDefinitions.ACTIVITIES},
        { title: 'PLANS', routerLink: RouterDefinitions.PLANS},
        { title: 'TEST', routerLink: RouterDefinitions.ACTIVITIES},
        { title: 'TSET', routerLink: RouterDefinitions.ACTIVITIES},
        { title: 'LOREM IPSUM', routerLink: RouterDefinitions.ACTIVITIES},
    ]
}
