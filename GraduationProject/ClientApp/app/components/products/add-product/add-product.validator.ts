import { ProductService } from './../../../services/product.service';
import { AbstractControl, ValidationErrors } from '@angular/forms';

export class ProductValidators{

    
    static  ValidateOnName(control: AbstractControl,services:ProductService): Promise<ValidationErrors|null>{
        
        return new Promise((resolve, reject) => {
          
            
            
        })
    }

    static ValidationOnSelect(control: AbstractControl): ValidationErrors | null{
        console.log(control.value);
        if (control.value == 0) {
          
            return { ValidationOnSelect: "please Select Product" }
        }
        else {
            return null
        }
       
    }
}