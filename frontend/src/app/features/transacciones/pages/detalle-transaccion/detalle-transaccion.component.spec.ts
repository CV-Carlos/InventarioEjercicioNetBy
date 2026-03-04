import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalleTransaccionComponent } from './detalle-transaccion.component';

describe('DetalleTransaccionComponent', () => {
  let component: DetalleTransaccionComponent;
  let fixture: ComponentFixture<DetalleTransaccionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DetalleTransaccionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalleTransaccionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
