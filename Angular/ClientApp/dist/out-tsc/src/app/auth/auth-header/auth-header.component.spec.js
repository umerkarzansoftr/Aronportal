import { async, TestBed } from '@angular/core/testing';
import { AuthHeaderComponent } from './auth-header.component';
describe('AuthHeaderComponent', () => {
    let component;
    let fixture;
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [AuthHeaderComponent]
        })
            .compileComponents();
    }));
    beforeEach(() => {
        fixture = TestBed.createComponent(AuthHeaderComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=auth-header.component.spec.js.map